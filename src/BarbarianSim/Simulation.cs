using HunterSim.GearSets;
using System;
using System.Linq;

namespace HunterSim
{
    public class Simulation
    {
        public readonly SimulationState State = new SimulationState();

        public Simulation(SimulationConfig config) => State.Config = config;

        public SimulationState Run()
        {
            // TODO: Start MP5 events
            // TODO: Start Spirit events
            // TODO: Raid DPS
            // TODO: Generate Report/Analysis
            // TODO: Pet DPS
            // TODO: Apply aspect of the hawk

            if (!State.Validate())
            {
                return State;
            }

            while (true)
            {
                ExecuteRotation();
                var nextEvent = GetNextEvent();

                State.CurrentTime = nextEvent.Timestamp;

                if (State.CurrentTime > State.Config.SimulationSettings.FightLength)
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
            // TODO: config setting for whether we are responsible for refreshing hunters mark
            // TODO: config setting for if/when we cast feign death
            // TODO: config settings for potion usage
            // TODO: configurable reaction time delays for all abilities
            if (AutoShot.CanUse(State))
            {
                AutoShot.Use(State);
            }
        }

        private EventInfo GetNextEvent() => State.Events.OrderBy(e => e.Timestamp).FirstOrDefault();
    }
}
