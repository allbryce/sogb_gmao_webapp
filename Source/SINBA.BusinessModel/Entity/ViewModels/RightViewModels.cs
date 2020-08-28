namespace Sinba.BusinessModel.Entity
{
    public class ProfilRightViewModel
    {
        public string CodeFonction { get; set; }
        public string Actions { get; set; }
    }

    public class SinbaResponse
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public object Data { get; set; }
    }

}
