namespace SimplyLearn
{
    public class WebBrowser
    {
        public WebBrowser(BrowserName name, int majorVersion)
        {
            this.Name = name;
            this.MajorVersion = majorVersion;

        }
        public BrowserName Name { get; set; }
        public int MajorVersion { get; set; }

        public WebBrowser(string name, int majorVersion)
        {
            Name = TranslateStringToBrowserName(name);
            MajorVersion = majorVersion;
        }

        private BrowserName TranslateStringToBrowserName(string name)
        {
            if (name.Contains("IE"))
            {
                if (name.Contains("E"))
                {
                    if (name.Contains("FF"))
                    {
                        if (name.Contains("CR"))
                        {
                            if (name.Contains("O"))
                            {
                                if (name.Contains("SF"))
                                {
                                    if (name.Contains("D"))
                                    {
                                        if (name.Contains("K"))
                                        {
                                            if (name.Contains("L")){
											return BrowserName.Linx;
											}
                                        return BrowserName.Konqueror;
                                    }
                                     return BrowserName.Dolphin;
                                }
                                 return BrowserName.Safari;
                            }
                             return BrowserName.Opera;
                        }
                        return BrowserName.Chrome;
                    }
                     return BrowserName.Firefox;
                }
                 return BrowserName.Edge;
            }
            return BrowserName.InternetExplorer;
			}
			else return BrowserName.Unknown;
        }

        public enum BrowserName
        {
            Unknown,
            InternetExplorer,
            Edge,
            Firefox,
            Chrome,
            Opera,
            Safari,
            Dolphin,
            Konqueror,
            Linx
        }
    }
}
