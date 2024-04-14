namespace ReactWebManual.Server.Models
{
    public class Worker
    {
        public const int MAX_FIO_LENGTH = 250;
        private Worker(Guid id, string fio, string dr, string sex, string post, Boolean driverslicense) 
        { 
            Id = id;
            FIO = fio;
            DR = dr;
            Sex = sex;
            Post = post;
            DriversLicense = driverslicense;
        }

        public Guid Id { get;}    
        public string FIO { get;} = string.Empty;
        public string DR { get;} = string.Empty;
        public string Sex { get;} = string.Empty;
        public string Post { get;} = string.Empty;
        public Boolean DriversLicense { get;} = false;

        public static (Worker Worker, string Error) Create(Guid id, string fio, string dr, string sex, string post, Boolean driverslicense) 
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(fio) || fio.Length > MAX_FIO_LENGTH)
            {
                error = "FIO can not be empty or longer then 250 symbols";
            }

            var worker = new Worker(id, fio, dr, sex, post, driverslicense);

            return (worker, error);
        }

    }
}
