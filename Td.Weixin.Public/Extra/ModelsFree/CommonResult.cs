namespace Td.Weixin.Public.Extra.ModelsFree
{
    public class CommonResult
    {
        public int ret { get; set; }

        public string err_msg { get; set; }

        public bool IsSuccess
        {
            get { return ret == 0; }
        }
    }
}