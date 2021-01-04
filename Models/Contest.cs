using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
					

namespace CodeRacer.Models
{
    public class Competition
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<CompetitionFile> Inputs { get; set; }
        public IEnumerable<Submission> Submissions { get; set; }
        
    }
    public class File
    {
        public int Id { get; set; }
        public string Path { get; set; }
    }

    [Table("CompetitionFiles")]
    public class CompetitionFile : File {
        public int CompetitionId { get; set; }
        public Competition Owner { get; set; }
    }

    [Table("SubmissionFiles")]
    public class SubmissionFile : File {
        public int SubmissionId { get; set;}
        public Submission Owner { get; set; }
    }

    public class Submission
    {
        public int Id { get; set;}

        public IEnumerable<SubmissionFile> SubmissionFiles { get; set; }

        public int CompetitionId { get; set; }
        public Competition Owner { get; set; }
    }
}