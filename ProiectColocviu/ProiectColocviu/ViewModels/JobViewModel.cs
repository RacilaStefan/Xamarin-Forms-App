using EJobsMarket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EJobsMarket.ViewModels
{
    class JobViewModel : BaseViewModel
    {
        private string compensationMeasure;
        public string CompensationMeasure
        {
            get => compensationMeasure;
            set => SetProperty(ref compensationMeasure, value);
        }

        private string title;
        public string JobTitle
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private ExperienceTypes experienceRequired = (ExperienceTypes)(-1);
        public ExperienceTypes ExperienceRequired
        {
            get => experienceRequired;
            set => SetProperty(ref experienceRequired, value);
        }

        private JobTypes jobType = (JobTypes)(-1);
        public JobTypes JobType
        {
            get => jobType;
            set => SetProperty(ref jobType, value);
        }

        private CompensationTypes compensationType = (CompensationTypes)(-1);
        public CompensationTypes CompensationType
        {
            get => compensationType;
            set
            {
                SetProperty(ref compensationType, value);
                switch (compensationType)
                {
                    case CompensationTypes.Hourly: CompensationMeasure = "$/hour"; break;
                    case CompensationTypes.Daily: CompensationMeasure = "$/day"; break;
                    case CompensationTypes.Weekly: CompensationMeasure = "$/week"; break;
                    case CompensationTypes.Monthly: CompensationMeasure = "$/month"; break;
                    case CompensationTypes.Project: CompensationMeasure = "$/project"; break;
                    default:
                        break;
                }
            }
        }

        private float compensation;
        public float Compensation
        {
            get => compensation;
            set => SetProperty(ref compensation, value);
        }

        private bool remote;
        public bool Remote
        {
            get => remote;
            set => SetProperty(ref remote, value);
        }

        private string remoteString;
        public string RemoteString
        {
            get => remoteString;
            set => SetProperty(ref remoteString, value);
        }

        public JobViewModel()
        {

        }
    }
}
