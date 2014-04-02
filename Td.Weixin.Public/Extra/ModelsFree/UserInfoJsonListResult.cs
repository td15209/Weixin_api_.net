namespace Td.Weixin.Public.Extra.ModelsFree
{
    public class UserInfoJsonListResult
    {
        /// <summary>
        ///     请求结果描述
        /// </summary>
        internal UserInfoListJsonBaseResult base_resp { get; set; }

        /// <summary>
        ///     公众号帐户信息
        /// </summary>
        public MpUserInfo user_info { get; set; }


        public MpUserAcl user_acl { get; set; }
        public UserInfoListJsonPageMsg page_msg { get; set; }
        public int total_count { get; set; }
        public int verify_msg_count { get; set; }
        public string group_list { get; set; }
        public string contact_list { get; set; }
    }

    internal class UserInfoListJsonBaseResult
    {
        public int ret { get; set; }
        public string err_msg { get; set; }
        public int svr_time { get; set; }
        public string token { get; set; }
        public MpNav nav { get; set; }
        public int cgi_id { get; set; }
        public string media_ticket { get; set; }
    }

    /// <summary>
    ///     公众号拥有的菜单列表
    /// </summary>
    public class MpNav
    {
        public MpNavItem[] nav_item { get; set; }
    }

    /// <summary>
    ///     公众号拥有的菜单项描述
    /// </summary>
    public class MpNavItem
    {
        public string nav_id { get; set; }
        public int nav_flag { get; set; }
        public string primary_nav { get; set; }
        public int is_selected { get; set; }
    }

    /// <summary>
    ///     公众号帐户信息
    /// </summary>
    public class MpUserInfo
    {
        public string nick_name { get; set; }
        public long fake_id { get; set; }
        public int user_role { get; set; }
        public int mass_send_left { get; set; }
        public int have_channel { get; set; }
        public string alias { get; set; }
        public int sys_notify_cnt { get; set; }
        public int service_type { get; set; }
        public string notify_msg { get; set; }
        public string user_name { get; set; }
        public int is_wx_verify { get; set; }
        public int realname_status { get; set; }
        public int realname_type { get; set; }
        public int is_vip { get; set; }
        public int is_dev_user { get; set; }
        public int have_package { get; set; }
        public MpWbInfo wb_info { get; set; }
        public int is_verify_on { get; set; }
    }


    /// <summary>
    ///     公众号微博信息
    /// </summary>
    public class MpWbInfo
    {
        public string src { get; set; }
        public int verify_flag { get; set; }
        public string verify_info { get; set; }
        public string nick_name { get; set; }
        public string head_img_url { get; set; }
        public string verify_code { get; set; }
        public int manual_review { get; set; }
        public int can_sync { get; set; }
        public int verify_status { get; set; }
    }

    /// <summary>
    ///     公众号访问控制列表集
    /// </summary>
    public class MpUserAcl
    {
        public MpBaseAcl base_acl { get; set; }
        public MpMsgAcl msg_acl { get; set; }
        public MpIvrAcl ivr_acl { get; set; }
    }

    /// <summary>
    ///     公众号基本访问控制列表
    /// </summary>
    public class MpBaseAcl
    {
        public int can_mass_send { get; set; }
        public int can_public_channel { get; set; }
        public int can_verify_apply { get; set; }
        public int can_use_advanced_func { get; set; }
        public int can_use_biz_menu { get; set; }
        public int can_use_biz_ivr { get; set; }
        public int can_dev_apply { get; set; }
        public int can_use_merchant { get; set; }
        public int can_use_customer { get; set; }
        public int can_use_dev_reply { get; set; }
        public int can_access_merchant { get; set; }
        public int can_access_tmplmsg { get; set; }
        public int can_modify_info { get; set; }
        public int can_modify_ivr { get; set; }
        public int can_modify_biz_menu { get; set; }
    }

    /// <summary>
    ///     公众号消息访问控制列表
    /// </summary>
    public class MpMsgAcl
    {
        public int can_text_msg { get; set; }
        public int can_image_msg { get; set; }
        public int can_voice_msg { get; set; }
        public int can_video_msg { get; set; }
        public int can_app_msg { get; set; }
        public int can_app_msg_activity { get; set; }
        public int can_app_msg_comm { get; set; }
        public int can_app_msg_single { get; set; }
        public int can_app_msg_multi { get; set; }
        public int can_share_card { get; set; }
        public int can_commodity_app_msg { get; set; }
    }

    /// <summary>
    ///     公众号ivr访问控制列表(实际上我也不知道ivr指什么)
    /// </summary>
    public class MpIvrAcl
    {
        public int can_text_msg { get; set; }
        public int can_image_msg { get; set; }
        public int can_voice_msg { get; set; }
        public int can_video_msg { get; set; }
        public int can_app_msg { get; set; }
        public int can_app_msg_activity { get; set; }
        public int can_app_msg_comm { get; set; }
        public int can_app_msg_single { get; set; }
        public int can_app_msg_multi { get; set; }
        public int can_commodity_app_msg { get; set; }
    }

    /// <summary>
    ///     用户概要信息列表请求的当前请求的分页信息
    /// </summary>
    public class UserInfoListJsonPageMsg
    {
        public int page_index { get; set; }
        public int page_count { get; set; }
        public int page_size { get; set; }
    }
}