using Newtonsoft.Json;
using RedisHelp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedisSub
{
    public partial class Form1 : Form
    {
        RedisHelper redis = new RedisHelper(2);
        bool subState = false;
        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnsub_Click(object sender, EventArgs e)
        {
            if (!subState)
            {
                Task.Run(() => StartSub());

                this.btnsub.ForeColor = System.Drawing.Color.Green;
                this.btnsub.Text = "订阅中...";
            }
            else
            {
                Task.Run(() => StopSub());
                this.btnsub.ForeColor = System.Drawing.Color.Black;
                this.btnsub.Text = "订阅开始";
                
            }


        }

        //开始订阅
        private void StartSub()
        {
            subState = true;
            redis.Subscribe("Channel1", (channel,message) => SubHandler(channel,message));
        }
        //取消订阅
        private void StopSub()
        {
            subState = false;
            redis.Unsubscribe("Channel1");
        }

        private void SubHandler(RedisChannel channel,RedisValue message) 
        {
            if (lbMessage.Items.Count>10000)
            {
                lbMessage.Items.Clear();
            }
            lbNum.Text=(Convert.ToInt32(lbNum.Text)+1).ToString();
            lbMessage.Items.Add(message.ToString());

            Task.Run(() => insertSubData(message));
        }

        private void insertSubData(RedisValue message)
        {
            vw_Call_TelService v=JsonConvert.DeserializeObject<vw_Call_TelService>(message.ToString());
            redis.HashSetAsync("order", v.TelServiceCode, message.ToString());
            redis.HashDelete("orderlist", v.TelServiceCode);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbMessage.Items.Clear();
            lbNum.Text = "0";
        }
    }

    /// <summary>
    /// vw_Call_TelService:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public partial class vw_Call_TelService
    {
        public vw_Call_TelService()
        { }
        #region Model
        private long _id;
        private string _telservicecode;
        private DateTime? _calltime;
        private string _thecall;
        private DateTime? _submittime;
        private string _clientcode;
        private int? _autoflag;
        private string _courier;
        private DateTime? _printtime;
        private DateTime? _endservicetime;
        private string _remark;
        private string _editor;
        private DateTime? _editdate;
        private string _franchiser;
        private string _oldcode;
        private bool _stopflag;
        private string _stopeditor;
        private DateTime? _stopdate;
        private int? _outwhflag;
        private string _timeoutconclusion;
        private string _printuser;
        private DateTime? _outwhdate;
        private int? _timeout;
        private int? _rgflag;
        private int? _sumitcount;
        private int? _submitno;
        private string _todept;
        private int? _billsort;
        private string _deptcourier;
        private int? _courierflag;
        private int? _freetype;
        private decimal? _totalamount;
        private decimal? _discountamount;
        private int? _paystatus;
        private string _paytype;
        private int _month_int;
        private DateTime? _courierdate;
        private int? _signflag;
        private string _clientname;
        private string _address;
        private string _franchisercode;
        private int? _memberpoints;
        private string _personalized;
        private string _clientsortcode;
        private string _memberflag;
        private string _membercard;
        private string _clremark;
        private int? _deliverytime;
        private string _clientsortname;
        private string _couriername;
        private string _username;
        private string _autoflagname;
        private string _todeptname;
        private string _freename;
        private string _billname;
        private string _signTime;
        private bool _appraiseFlag;
        private int _autodispatchnum;
        private string _empPhone;

        public string EmpPhone
        {
            get { return _empPhone; }
            set { _empPhone = value; }
        }
        private string _empName;

        public string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
        }

        public int autodispatchnum
        {
            get { return _autodispatchnum; }
            set { _autodispatchnum = value; }
        }

        public bool AppraiseFlag
        {
            get { return _appraiseFlag; }
            set { _appraiseFlag = value; }
        }
        private string _positionX;

        public string PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
        private string _positionY;

        public string PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }
        public string SignTime
        {
            get { return _signTime; }
            set { _signTime = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TelServiceCode
        {
            set { _telservicecode = value; }
            get { return _telservicecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CallTime
        {
            set { _calltime = value; }
            get { return _calltime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TheCall
        {
            set { _thecall = value; }
            get { return _thecall; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SubmitTime
        {
            set { _submittime = value; }
            get { return _submittime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientCode
        {
            set { _clientcode = value; }
            get { return _clientcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AutoFlag
        {
            set { _autoflag = value; }
            get { return _autoflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Courier
        {
            set { _courier = value; }
            get { return _courier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PrintTime
        {
            set { _printtime = value; }
            get { return _printtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndServiceTime
        {
            set { _endservicetime = value; }
            get { return _endservicetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Editor
        {
            set { _editor = value; }
            get { return _editor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Editdate
        {
            set { _editdate = value; }
            get { return _editdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Franchiser
        {
            set { _franchiser = value; }
            get { return _franchiser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldCode
        {
            set { _oldcode = value; }
            get { return _oldcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool StopFlag
        {
            set { _stopflag = value; }
            get { return _stopflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StopEditor
        {
            set { _stopeditor = value; }
            get { return _stopeditor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StopDate
        {
            set { _stopdate = value; }
            get { return _stopdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OutWhFlag
        {
            set { _outwhflag = value; }
            get { return _outwhflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TimeOutConclusion
        {
            set { _timeoutconclusion = value; }
            get { return _timeoutconclusion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrintUser
        {
            set { _printuser = value; }
            get { return _printuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutWhDate
        {
            set { _outwhdate = value; }
            get { return _outwhdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TimeOut
        {
            set { _timeout = value; }
            get { return _timeout; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RgFlag
        {
            set { _rgflag = value; }
            get { return _rgflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SumitCount
        {
            set { _sumitcount = value; }
            get { return _sumitcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubmitNO
        {
            set { _submitno = value; }
            get { return _submitno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ToDept
        {
            set { _todept = value; }
            get { return _todept; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BillSort
        {
            set { _billsort = value; }
            get { return _billsort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeptCourier
        {
            set { _deptcourier = value; }
            get { return _deptcourier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CourierFlag
        {
            set { _courierflag = value; }
            get { return _courierflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FreeType
        {
            set { _freetype = value; }
            get { return _freetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TotalAmount
        {
            set { _totalamount = value; }
            get { return _totalamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DiscountAmount
        {
            set { _discountamount = value; }
            get { return _discountamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? paystatus
        {
            set { _paystatus = value; }
            get { return _paystatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string paytype
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Month_int
        {
            set { _month_int = value; }
            get { return _month_int; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CourierDate
        {
            set { _courierdate = value; }
            get { return _courierdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SignFlag
        {
            set { _signflag = value; }
            get { return _signflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientName
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FranchiserCode
        {
            set { _franchisercode = value; }
            get { return _franchisercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MemberPoints
        {
            set { _memberpoints = value; }
            get { return _memberpoints; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Personalized
        {
            set { _personalized = value; }
            get { return _personalized; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientSortCode
        {
            set { _clientsortcode = value; }
            get { return _clientsortcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MemberFlag
        {
            set { _memberflag = value; }
            get { return _memberflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MemberCard
        {
            set { _membercard = value; }
            get { return _membercard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClRemark
        {
            set { _clremark = value; }
            get { return _clremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DeliveryTime
        {
            set { _deliverytime = value; }
            get { return _deliverytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientSortName
        {
            set { _clientsortname = value; }
            get { return _clientsortname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CourierName
        {
            set { _couriername = value; }
            get { return _couriername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AutoFlagName
        {
            set { _autoflagname = value; }
            get { return _autoflagname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ToDeptName
        {
            set { _todeptname = value; }
            get { return _todeptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Freename
        {
            set { _freename = value; }
            get { return _freename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Billname
        {
            set { _billname = value; }
            get { return _billname; }
        }
        #endregion Model

    }
}
