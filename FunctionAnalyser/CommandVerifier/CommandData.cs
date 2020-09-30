namespace CommandVerifier
{
    public class CommandData
    {
        /// <summary>
        /// Command ended with an optional parameter.
        /// </summary>
        public bool EndedOptional { get; set; }
        public bool EscapeLoop { get; set; }
        public bool ExpectCommand { get; set; }

        /// <summary>
        /// First requirement was passed. (This may also be the only one)
        /// </summary>
        public bool PassedFirstRequirement { get; set; }

        /// <summary>
        /// All requirements were passed.
        /// </summary>
        public bool PassedAllRequirements { get; set; }
        public bool DisableForcedPath { get; set; }

        /// <summary>
        /// Loop the command contents.
        /// </summary>
        public bool LoopContents { get; set; }

        public CommandData() => Reset();
        public void Reset()
        {
            EndedOptional = false;
            EscapeLoop = false;
            ExpectCommand = true;
            PassedFirstRequirement = false;
            PassedAllRequirements = false;
            DisableForcedPath = false;
            LoopContents = false;
        }
    }
}
