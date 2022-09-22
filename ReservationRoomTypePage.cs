public ReservationRoomTypePage(ReservationViewModel model)
        {
            InitializeComponent();

            _Model = model;
            BindingContext = _Model;


            //Show RoomType and Status
            Dictionary<string, string[]> myDictionary3 = _Model.getRoomStatusData(_Model.RoomTypesSel, _Model.StartTimeNames);
            //Console.WriteLine("Room Type arrya Size: " + _Model.RoomTypesSel.Length);
            //Console.WriteLine("Start time arrya Size: " + _Model.StartTimeNames.Length);
            //Console.WriteLine("Printing Room Status Dictionary");
            //Console.WriteLine();
            foreach (KeyValuePair<string, string[]> kvp in myDictionary3)
            {
                Console.WriteLine(kvp.Key);
                Console.WriteLine();

                foreach (string status in kvp.Value)
                {

                    Console.Write(status);
                }
                Console.WriteLine();

                DataGridColumn dataGridColumn = new DataGridColumn()
                {
                    Title = kvp.Key
                   
                };
                dataColumn.Columns.Add(dataGridColumn);

            }
        
            //foreach (KeyValuePair<string, string[]> kvp in myDictionary3)
            //{

            //    for (int i = 0; i < kvp.Value.Length; i++)
            //    {
            //        DataGridColumn dataGridColumn = new DataGridColumn()
            //        {
            //            Title = kvp.Key,
            //            PropertyName = kvp.Value[i],
            //        };
            //        dataColumn.Columns.Add(dataGridColumn);

            //    }
            //}
            //foreach (KeyValuePair<string, string[]> kvp in myDictionary3)
            //{
            //    for (int i = 0; i < kvp.Value.Length; i++)
            //    {
            //        col1.Title = kvp.Key;
            //        col1.PropertyName = kvp.Value[i];
            //    }
            //}
        }


//For Service//

//public Task<bool> CheckFreeRoom(CheckFree data)
        //{
        //    return Task.Run(() => {
        //        try
        //        {
        //            Rpc.SetHeader(HEADER_CID, GlobalVar.ClientID);
        //            var ret = Rpc.Execute(GlobalVar.WebServerAddress, TIMEOUT, API_TEST,
        //                (XmlRpcInt)data.StoreCode, (XmlRpcString)data.MemberCode);

        //            var s = ret[0] as XmlRpcStruct;

        //            var code = int.Parse((XmlRpcString)s["error_code"]);
        //            if (code == ERROR_SUCCESS)
        //            {
        //                return true;
        //            }
        //        }
        //        catch { }
        //        return false;
        //    });
        //}


foreach (KeyValuePair<string, string[]> kvp in myDictionary3)
            {

                DataGridColumn dataGridColumn = new DataGridColumn();
                dataGridColumn.Title = kvp.Key;
                Console.WriteLine(kvp.Key);
                Console.WriteLine();

                List<Status> statuses = new List<Status>();

                foreach (string status in kvp.Value)
                {

                    statuses.Add(new Status { status = status });
                }
                    dataGridColumn.Width = 50;



                    dataGridColumn.BindingContext = statuses;
                    dataGridColumn.SetBinding(DataGridColumn.PropertyNameProperty, new Binding("status"));



                dataGrid.Columns.Add(dataGridColumn);

            }
