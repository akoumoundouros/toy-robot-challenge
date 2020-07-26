namespace ConsoleToyRobotChallenge.SimulatorApp
{
    public abstract class SimulatorApp
    {
        /// <summary>
        /// ignore command case?
        /// </summary>
        protected bool _ignoreCmdCase = false;

        /// <summary>
        /// default ctor
        /// </summary>
        /// <param name="ignoreCmdCase">ignore case?</param>
        public SimulatorApp(bool ignoreCmdCase)
        {
            this._ignoreCmdCase = ignoreCmdCase;
        }


        /// <summary>
        /// implement run in derived classes please
        /// </summary>
        /// <returns></returns>
        public abstract void Run();
    }
}
