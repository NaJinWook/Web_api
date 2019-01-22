using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _20181207.Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _20181207.Controller
{
    class MainController
    {
        private Commons comm;
        private Panel head, contents, view, controller;
        private Button btn1, btn2, btn3;
        private ListView listView;
        private TextBox textBox1, textBox2, textBox3, textBox4, textBox5, textBox6;
        private Form parentForm, tagetForm;
        private Hashtable hashtable;
        WebClient client;

        public MainController(Form parentForm)
        {
            this.parentForm = parentForm;
            comm = new Commons();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 100);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 1);
            hashtable.Add("name", "head");
            head = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 700);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 100);
            hashtable.Add("color", 4);
            hashtable.Add("name", "contents");
            contents = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 20);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 3);
            hashtable.Add("name", "controller");
            controller = comm.getPanel(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 680);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 20);
            hashtable.Add("color", 0);
            hashtable.Add("name", "view");
            view = comm.getPanel(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 100);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "btn1");
            hashtable.Add("text", "입력");
            hashtable.Add("click", (EventHandler) SetInsert);
            btn1 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 400);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "btn2");
            hashtable.Add("text", "수정");
            hashtable.Add("click", (EventHandler) SetUpdate);
            btn2 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 700);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "btn3");
            hashtable.Add("text", "삭제");
            hashtable.Add("click", (EventHandler) SetDelete);
            btn3 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("color", 0);
            hashtable.Add("name", "listView");
            hashtable.Add("click", (MouseEventHandler)listView_click);
            listView = comm.getListView(hashtable, view);

            hashtable = new Hashtable();
            hashtable.Add("width", 45);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox1");
            hashtable.Add("enabled", false);
            textBox1 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 252);
            hashtable.Add("pX", 45);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox2");
            hashtable.Add("enabled", true);
            textBox2 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 452);
            hashtable.Add("pX", 295);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox3");
            hashtable.Add("enabled", true);
            textBox3 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 82);
            hashtable.Add("pX", 745);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox4");
            hashtable.Add("enabled", false);
            textBox4 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 80);
            hashtable.Add("pX", 826);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox5");
            hashtable.Add("enabled", false);
            textBox5 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 80);
            hashtable.Add("pX", 906);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox6");
            hashtable.Add("enabled", false);
            textBox6 = comm.getTextBox(hashtable, controller);

            GetSelect();
        }

        private void GetSelect()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            listView.Clear();

            listView.Columns.Add("번호", 45, HorizontalAlignment.Center);        /* Notice 번호 */
            listView.Columns.Add("제목", 250, HorizontalAlignment.Center);      /* Notice 제목 */
            listView.Columns.Add("내용", 450, HorizontalAlignment.Center);   /* Notice 내용 */
            listView.Columns.Add("삭제여부", 80, HorizontalAlignment.Center);     /* Notice 삭제 여부 */
            listView.Columns.Add("작성일", 80, HorizontalAlignment.Center);     /* Notice 작성 현재날짜 */
            listView.Columns.Add("수정일", 80, HorizontalAlignment.Center);     /* Notice 수정 현재날짜 */

            // 보여 주기 가상 데이터 -> WebAPI를 이용하여 데이터 가져올것!
            //listView.Items.Add(new ListViewItem(new string[] { "3", "제목3", "내용3", "Winform", "2018-12-07", "2016-12-07" }));
            //listView.Items.Add(new ListViewItem(new string[] { "2", "제목2", "내용2", "스마트", "2018-12-06", "2016-12-07" }));
            //listView.Items.Add(new ListViewItem(new string[] { "1", "제목1", "내용1", "관리자", "2018-12-05", "2016-12-07" }));

            client = new WebClient();
            NameValueCollection data = new NameValueCollection();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Encoding = Encoding.UTF8;

            string url = "http://192.168.3.119:80/api/Select";
            Stream result = client.OpenRead(url);

            StreamReader sr = new StreamReader(result);
            string str = sr.ReadToEnd();

            ArrayList jList = JsonConvert.DeserializeObject<ArrayList>(str);
            ArrayList list = new ArrayList();
            foreach (JObject row in jList)
            {
                Hashtable ht = new Hashtable();
                foreach (JProperty col in row.Properties())
                {
                    ht.Add(col.Name, col.Value);
                }
                list.Add(ht);
            }
            foreach (Hashtable ht in list)
            {
                listView.Items.Add(new ListViewItem(new string[] { ht["nNo"].ToString(), ht["nTitle"].ToString(), ht["nContents"].ToString(), ht["delYn"].ToString(), ht["regDate"].ToString(), ht["modDate"].ToString() }));
            }
        }

        private void SetInsert(object o, EventArgs e)
        {
            client = new WebClient();
            NameValueCollection data = new NameValueCollection();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Encoding = Encoding.UTF8;

            string url = "http://192.168.3.119:80/api/Insert";
            string method = "POST";

            data.Add("nTitle", textBox2.Text);
            data.Add("nContents", textBox3.Text);

            byte[] result = client.UploadValues(url, method, data);
            string strResult = Encoding.UTF8.GetString(result);

            GetSelect();
        }
        private void SetUpdate(object o, EventArgs e)
        {
            client = new WebClient();
            NameValueCollection data = new NameValueCollection();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Encoding = Encoding.UTF8;

            string url = "http://192.168.3.119:80/api/Update";
            string method = "POST";

            data.Add("nNo", textBox1.Text);
            data.Add("nTitle", textBox2.Text);
            data.Add("nContents", textBox3.Text);

            byte[] result = client.UploadValues(url, method, data);
            string strResult = Encoding.UTF8.GetString(result);

            GetSelect();
        }
        private void SetDelete(object o, EventArgs e)
        {
            client = new WebClient();
            NameValueCollection data = new NameValueCollection();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Encoding = Encoding.UTF8;

            string url = "http://192.168.3.119:80/api/Delete";
            string method = "POST";

            data.Add("nNo", textBox1.Text);
            data.Add("delYn", textBox4.Text);

            byte[] result = client.UploadValues(url, method, data);
            string strResult = Encoding.UTF8.GetString(result);

            GetSelect();
        }
        private void listView_click(object o, EventArgs a)
        {
            ListView lv = (ListView)o;
            ListView.SelectedListViewItemCollection itemGroup = lv.SelectedItems;
            ListViewItem item = itemGroup[0];

            textBox1.Text = item.SubItems[0].Text;
            textBox2.Text = item.SubItems[1].Text;
            textBox3.Text = item.SubItems[2].Text;
            textBox4.Text = item.SubItems[3].Text;
            textBox5.Text = item.SubItems[4].Text;
            textBox6.Text = item.SubItems[5].Text;
        }
    }
}
