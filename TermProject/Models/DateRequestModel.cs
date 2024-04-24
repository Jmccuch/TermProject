using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TermProject.Models
{
    public class DateRequestModel
    {

        private DateTime date;
        private string time;
        private string description;


        public DateRequestModel(DateTime date, string time, string description)
        {

            this.date = date;
            this.time = time;
            this.description = description;

        }

        [BindProperty, DataType(DataType.Date)]
        public DateTime Date
        {

            get { return date; }

            set { date = value; }
        }

        public string Time
        {
            get { return time; }

            set { time = value; }
        }

        public string Description
        {
            get { return description; }

            set { description = value; }
        }
    }
}
