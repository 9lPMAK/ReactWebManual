namespace WorkerStore.DataAccess.Entites
{
    public class WorkerEntity
    {
        public Guid Id { get; set; }
        public string FIO { get; set; } = string.Empty;
        public string DR { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
        public Boolean DriversLicense { get; set; } = false;
    }
}
