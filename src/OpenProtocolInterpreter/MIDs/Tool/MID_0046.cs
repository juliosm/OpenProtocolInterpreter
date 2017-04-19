﻿namespace OpenProtocolInterpreter.MIDs.Tool
{
    /// <summary>
    /// MID: Set primary tool request
    /// Description: 
    ///     This message is sent by the integrator in order to set tool data.
    ///     Warning 1: this MID requires programming control (see 4.4 Programming control).
    ///     Warning 2: the new configuration will not be active until the next controller reboot!
    /// Message sent by: Integrator
    /// Answer: MID 0005 Command accepted or MID 0004 Command error, 
    ///         Programming control not granted or Invalid data (value not supported by controller)
    /// </summary>
    public class MID_0046 : MID, ITool
    {
        private const int length = 24;
        private const int mid = 46;
        private const int revision = 1;

        public PrimaryTools PrimaryTool { get; set; }

        public MID_0046() : base(length, mid, revision)
        {
            this.registerDatafields();
        }

        public MID_0046(IMID nextTemplate) : base(length, mid, revision)
        {
            this.registerDatafields();
            this.nextTemplate = nextTemplate;
        }

        public override string buildPackage()
        {
            this.RegisteredDataFields[(int)DataFields.PRIMARY_TOOL].Value = (int)this.PrimaryTool;
            return base.buildPackage();
        }

        public override MID processPackage(string package)
        {
            if (base.isCorrectType(package))
            {
                base.processPackage(package);
                this.PrimaryTool = (PrimaryTools)this.RegisteredDataFields[(int)DataFields.PRIMARY_TOOL].ToInt32();
                return this;
            }

            return this.nextTemplate.processPackage(package);
        }

        private void registerDatafields()
        {
            this.RegisteredDataFields.Add(new DataField((int)DataFields.PRIMARY_TOOL, 20, 2));

        }

        public enum PrimaryTools
        {
            /// <summary>
            /// Invalid for IRC-Controller
            /// </summary>
            CABLE = 01,
            IRC_B = 02,
            IRC_W = 03
        }

        public enum DataFields
        {
            PRIMARY_TOOL
        }
    }
}
