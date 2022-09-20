public ReservationRoomTypePage(ReservationViewModel model)
        {
            InitializeComponent();

            _Model = model;
            BindingContext = _Model;


            //Show RoomType and Status
            Dictionary<string, string[]> myDictionary3 = _Model.getRoomStatusData(_Model.RoomTypesSel, _Model.StartTimeNames);
            Console.WriteLine("Room Type arrya Size: " + _Model.RoomTypesSel.Length);
            Console.WriteLine("Start time arrya Size: " + _Model.StartTimeNames.Length);
            Console.WriteLine("Printing Room Status Dictionary");
            Console.WriteLine();
            foreach (KeyValuePair<string, string[]> kvp in myDictionary3)
            {
                Console.WriteLine(kvp.Key);
                Console.WriteLine();

                foreach (string status in kvp.Value)
                {

                    Console.Write(status);
                }
                Console.WriteLine();

              DataGridColumn dataGridColumn = new DataGridColumn();
              {
                    Title = kvp.Key;
                    
              }
              dataColumn.Children.Add(dataGridColumn);
            }
        }
