using System.Diagnostics;
using System.Text;
using BarbarianSim.Events;

namespace BarbarianSim;

public class CombatLog
{
    private readonly SimulationState _state;

    public CombatLog(SimulationState state) => _state = state;

    public void Show()
    {
        var html = GenerateHtml();
        File.WriteAllText("CombatLog.html", html);

        using var p = new Process
        {
            StartInfo = new(@"CombatLog.html") { UseShellExecute = true }
        };
        p.Start();
    }

    private string GenerateHtml()
    {
        var sb = new StringBuilder();
        sb.AppendLine("<!doctype html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("  <head>");
        sb.AppendLine("    <meta charset=\"utf-8\">");
        sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
        sb.AppendLine("    <title>Diablo 4 Sim</title>");
        sb.AppendLine("    <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-4bw+/aepP/YC94hEpVNVgiZdgIC5+VKNBQNGCHeKRQN+PtmoHDEXuppvnDJzQIu9\" crossorigin=\"anonymous\">");
        sb.AppendLine("  </head>");
        sb.AppendLine("  <body>");
        sb.AppendLine("    <script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/js/bootstrap.bundle.min.js\" integrity=\"sha384-HwwvtgBNo3bZJJLYd8oVXjrBZt8cqVSpeBNS5n7C8IVInixGAoxmnlMuBnhbgrkm\" crossorigin=\"anonymous\"></script>");
        sb.AppendLine("    <div class=\"EventFilter\">");
        sb.AppendLine("      <ul class=\"list-group\" style=\"width: 400px; padding: 10px\">");

        foreach (var eventGroup in _state.ProcessedEvents.GroupBy(x => x.GetType().Name).OrderByDescending(x => x.Count()))
        {
            RenderEventFilter(eventGroup.Key, eventGroup.Count(), sb);
        }

        sb.AppendLine("      </ul>");
        sb.AppendLine("    </div>");
        sb.AppendLine("    <div class=\"accordion\" id=\"accordionExample\" style=\"width: 1200px; padding: 10px\">");

        foreach (var e in _state.ProcessedEvents)
        {
            RenderEvent(e, sb);
        }

        sb.AppendLine("    </div>");
        sb.AppendLine("    <script>");
        sb.AppendLine("      document.querySelectorAll('.EventFilterCheckbox').forEach(function (checkbox) {");
        sb.AppendLine("        checkbox.addEventListener('change', function (event) {");
        sb.AppendLine("          var eventClass = event.target.id.replace('Checkbox', '');");
        sb.AppendLine("          var eventElements = document.querySelectorAll('.' + eventClass);");
        sb.AppendLine("          if (event.target.checked) {");
        sb.AppendLine("            eventElements.forEach(function (eventElement) {");
        sb.AppendLine("              eventElement.style.display = 'block';");
        sb.AppendLine("            });");
        sb.AppendLine("          } else {");
        sb.AppendLine("            eventElements.forEach(function (eventElement) {");
        sb.AppendLine("              eventElement.style.display = 'none';");
        sb.AppendLine("            });");
        sb.AppendLine("          }");
        sb.AppendLine("        });");
        sb.AppendLine("      });");
        sb.AppendLine("    </script>");
        sb.AppendLine("    <style>");
        sb.AppendLine("      .EventFilter, .accordion {");
        sb.AppendLine("        height: 100vh;");
        sb.AppendLine("        float: left;");
        sb.AppendLine("        overflow-y: auto;");
        sb.AppendLine("      }");
        sb.AppendLine("    </style>");
        sb.AppendLine("  </body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    private void RenderEventFilter(string eventType, int count, StringBuilder sb)
    {
        sb.AppendLine($"<li class=\"list-group-item\">");
        sb.AppendLine($"  <input class=\"form-check-input me-1 EventFilterCheckbox\" type=\"checkbox\" id=\"{eventType}Checkbox\" checked>");
        sb.AppendLine($"  <label class=\"form-check-label\" for=\"{eventType}Checkbox\">{eventType} ({count})</label>");
        sb.AppendLine($"</li>");
    }

    private void RenderEvent(EventInfo e, StringBuilder sb)
    {
        var eventGuid = Guid.NewGuid();

        sb.AppendLine($"<div class=\"accordion-item {e.GetType().Name}\">");
        sb.AppendLine($"  <div class=\"accordion-header\">");
        sb.AppendLine($"    <button class=\"accordion-button collapsed py-2\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#{eventGuid}\" style=\"padding-left: 5px\">");
        sb.AppendLine($"      <span class=\"px-2 fw-semibold bg-success-subtle border border-success-subtle rounded-2 mx-2\">{e.Timestamp:F1}</span>{e}");
        sb.AppendLine($"    </button>");
        sb.AppendLine($"  </div>");
        sb.AppendLine($"  <div id=\"{eventGuid}\" class=\"accordion-collapse collapse\">");
        sb.AppendLine($"    <div class=\"accordion-body\" style=\"line-height: 15px\">");
        sb.AppendLine($"      <code>");

        if (e.VerboseLog.Any())
        {
            foreach (var line in e.VerboseLog)
            {
                sb.AppendLine($"        {line}<br/>");
            }
        }
        else
        {
            sb.AppendLine($"        [ No log data for this event ]");
        }

        sb.AppendLine($"      </code>");
        sb.AppendLine($"    </div>");
        sb.AppendLine($"  </div>");
        sb.AppendLine($"</div>");
    }
}
