namespace HRMIS_Dapper_API.Models
{
    public class BadgeDataModel
    {
        public DateTime PunchDate { get; set; }
        public string PunchNo { get; set; }  = string.Empty;
        public string DeviceNo { get; set; } = string.Empty;
        //public bool? reader { get; set; }
        //public bool? door_state { get; set; }
        //public int? error_code { get; set; }
        //public int? card_level { get; set; }
        //public int? block { get; set; }
        //public int? manual { get; set; }
        //public long Trans_No { get; set; }
        //public long? user_code { get; set; }
        //public DateTime? entry_date { get; set; }
        //public int? locked { get; set; }
        //public long? altTrans { get; set; }
        //public string card_no2 { get; set; } = string.Empty;
        //public long? CardID { get; set; }
    }
}
