namespace anskus.Application.DTOs.Response
{
   public record LoginResponse(bool Flag=false,string Message=null!,string token=null!,string RefreshToken=null!);
}
