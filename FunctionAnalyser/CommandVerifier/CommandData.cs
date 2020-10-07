namespace CommandVerifier
{
    public class CommandData
    {
        public bool EndedOptional { get; set; }
        public bool EscapeLoop { get; set; }
        public bool ExpectCommand { get; set; }
        public bool PassedFirstRequirement { get; set; }
        public bool PassedAllRequirements { get; set; }
        public bool DisableForcedPath { get; set; }
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
