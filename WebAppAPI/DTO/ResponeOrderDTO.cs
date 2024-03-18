namespace WebAppAPI.DTO
{
    public class ResponeOrderDTO

    {
        public double TotalPrice { get; set; }
        public int MachineID { get; set; }
        public int StoreID { get; set; }
        public int OrderImageID { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
