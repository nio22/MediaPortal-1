using MediaPortal.GUI.Library;

namespace ProcessPlugins.ExternalDisplay
{
    /// <summary>
    /// This <see cref="IDisplay"/> implementation can be used to send the display lines this 
    /// control produces to LCDSmartie, by exposing the lines as properties in the 
    /// GUIPropertyManager, where they can be picked up by the LCDSmartie plugin 
    /// by AllenConquest 
    /// </summary>
    public class None : IDisplay
    {
        private string[] lines;
        private int row=0;
        private int col=0;

        public None()
        {
        }

        /// <summary>
        /// This method is called by MP to initialize our plugin
        /// </summary>
        public void Start()
        {
            //don't do anything
        }

        /// <summary>
        /// This method is called by MP to shutdown our plugin
        /// </summary>
        public void Stop()
        {
            //TODO: remove our properties
        }

        /// <summary>
        /// Instructs our plugin to display the indicated message on the indicated line.
        /// Or (in our case) set the value of the property #externaldisplay.lineX where X
        /// is the indicated line.
        /// </summary>
        /// <param name="_line">The line to show the message on</param>
        /// <param name="_message">The message to show</param>
        public void SetLine(int _line, string _message)
        {
            GUIPropertyManager.SetProperty("#externaldisplay.line"+_line.ToString(),_message);
        }

        /// <summary>
        /// Cleans-up all used resources
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        public void Configure()
        {
        }

        public void Initialize(string _port, int _lines, int _cols, int _time, int _linesG, int _colsG, int _timeG, bool _backLight, int _contrast)
        {
            lines = new string[_lines];
            Clear();
        }

        public void SetPosition(int x, int y)
        {
            row=y;
            col=x;
        }

        public void SendText(string _text)
        {
            int j=0;
            char[] text = lines[row].ToCharArray();
            for(int i=col; i<text.Length; i++)
                text[i]=_text[j++];
            lines[row] = new string(text);
        }


        public void Clear()
        {
            for(int i=0; i<Settings.Instance.TextHeight; i++)
                lines[i]=new string(' ',Settings.Instance.TextWidth);
        }

        public string Name
        {
          get { return "PropertySetter"; }
        }

        public string Description
        {
            get { return "MediaPortal Property Setter V1.0"; }
        }

        public bool SupportsText
        {
            get { return true; }
        }

        public bool SupportsGraphics
        {
            get { return false; }
        }
    }
}
