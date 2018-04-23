using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Company
    {
        public static IList<Room> OfficeRooms
        {
            get
            {
                if (HttpContext.Current.Session["OfficeRooms"] == null)
                    HttpContext.Current.Session["OfficeRooms"] = GenerateOfficeRooms();
                return (IList<Room>)HttpContext.Current.Session["OfficeRooms"];
            }
        }
       
        public static Room GetRoomByID(int id)
        {
            return (from room in OfficeRooms where room.ID == id select room).SingleOrDefault<Room>();
        }
        public static int GenerateRoomID()
        {
            return OfficeRooms.Count > 0 ? OfficeRooms.Last().ID + 1 : 0;
        }
        
        public static void InsertRoom(Room room)
        {
            if (room != null)
            {
                room.ID = GenerateRoomID();
                OfficeRooms.Add(room);
            }
        }
        public static void UpdateRoom(Room room)
        {
            Room editableRoom = GetRoomByID(room.ID);
            if (editableRoom != null)
            {
                editableRoom.ID = room.ID;
                editableRoom.Number = room.Number;
                editableRoom.Floor = room.Floor;
                editableRoom.IsReserved = room.IsReserved;
            }
        }
        public static void RemoveRoomByID(int id)
        {
            Room editableRoom = GetRoomByID(id);
            if (editableRoom != null)
                OfficeRooms.Remove(editableRoom);
        }
        
        static IList<Room> GenerateOfficeRooms()
        {
            List<Room> rooms = new List<Room>();
            rooms.Add(
                new Room()
                {
                    ID = 0,
                    Number = 101,
                    Floor = 5,
                    IsReserved = false
                }
            );
            rooms.Add(
                new Room()
                {
                    ID = 1,
                    Number = 206,
                    Floor = 2,
                    IsReserved = true
                }
            );
            rooms.Add(
                new Room()
                {
                    ID = 2,
                    Number = 159,
                    Floor = 5,
                    IsReserved = false
                }
            );
            rooms.Add(
                new Room()
                {
                    ID = 3,
                    Number = 395,
                    Floor = 3,
                    IsReserved = false
                }
            );
            rooms.Add(
                new Room()
                {
                    ID = 4,
                    Number = 42,
                    Floor = 4,
                    IsReserved = false
                }
            );
            rooms.Add(
                new Room()
                {
                    ID = 5,
                    Number = 322,
                    Floor = 7,
                    IsReserved = true
                }
            );
            rooms.Add(
                new Room()
                {
                    ID = 6,
                    Number = 532,
                    Floor = 2,
                    IsReserved = true
                }
            );
            return rooms;
        }
    }

    public class Room
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public bool IsReserved { get; set; }
    }
}