﻿using OpenProtocolInterpreter.Converters;
using System.Collections.Generic;

namespace OpenProtocolInterpreter.Job.Advanced
{
    /// <summary>
    /// MID: Job off
    /// Description: Set the controller in Job off mode or reset the Job off mode.
    /// Message sent by: Integrator
    /// Answer: MID 0005 Command accepted
    /// </summary>
    public class MID_0130 : Mid, IAdvancedJob
    {
        private readonly IValueConverter<bool> _boolConverter;
        private const int LAST_REVISION = 1;
        public const int MID = 130;

        /// <summary>
        /// <para>False => Set Job Off</para>
        /// <para>True => Reset Job Off</para> 
        /// </summary>
        public bool JobOffStatus
        {
            get => RevisionsByFields[2][(int)DataFields.JOB_OFF_STATUS].GetValue(_boolConverter.Convert);
            set => RevisionsByFields[2][(int)DataFields.JOB_OFF_STATUS].SetValue(_boolConverter.Convert, value);
        }

        public MID_0130() : base(MID, LAST_REVISION)
        {
            _boolConverter = new BoolConverter();
        }

        public MID_0130(bool jobOffStatus) : this()
        {
            JobOffStatus = jobOffStatus;
        }

        internal MID_0130(IMid nextTemplate) : this() => NextTemplate = nextTemplate;

        protected override Dictionary<int, List<DataField>> RegisterDatafields()
        {
            return new Dictionary<int, List<DataField>>()
            {
                {
                    1, new List<DataField>()
                            {
                                new DataField((int)DataFields.JOB_OFF_STATUS, 20, 1, false),
                            }
                }
            };
        }

        public enum DataFields
        {
            JOB_OFF_STATUS
        }
    }
}