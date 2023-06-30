using BarbarianSim.Abilities;
using BarbarianSim.Config;

namespace BarbarianSim
{
    public class Simulation
    {
        public SimulationState State { get; init; }

        public Simulation(SimulationConfig config) => State = new SimulationState(config);

        public SimulationState Run()
        {
            if (!State.Validate())
            {
                return State;
            }

            while (true)
            {
                ExecuteRotation();
                var nextEvent = GetNextEvent();

                State.CurrentTime = nextEvent.Timestamp;

                if (State.EnemyLife <= 0)
                {
                    return State;
                }

                while (nextEvent != null && nextEvent.Timestamp == State.CurrentTime)
                {
                    State.Events.Remove(nextEvent);
                    nextEvent.ProcessEvent(State);

                    EventPublisher.PublishEvent(nextEvent, State);

                    nextEvent = GetNextEvent();
                }
            }

            throw new Exception("This should never happen");
        }

        private void ExecuteRotation()
        {
            if (LungingStrike.CanUse(State))
            {
                LungingStrike.Use(State);
            }
        }

        private EventInfo GetNextEvent() => State.Events.OrderBy(e => e.Timestamp).FirstOrDefault();
    }
}
