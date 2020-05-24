using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("install")]
    public class InstallArguments
    {
        [Argument(ShortName = "-b", LongName = "-binary", Help = "The path to the binary to install eg. -b ./ghi")]
        public string Binary { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Binary);
        }
    }
}