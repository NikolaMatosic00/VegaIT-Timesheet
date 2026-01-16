using VegaIT.Timesheet.Persistence.Models.Enumerations;

namespace VegaIT.Timesheet.Persistence.Models;

    public class Project
    {
		
	    public Guid ProjectId { get; set; }

        public Guid Client { get; set; }

        public Guid Leader { get; set; }

        public String ProjectName { get; set; }

        public String Description { get; set; }
	
	    public EStatusOfProject Status { get; set; }


}

