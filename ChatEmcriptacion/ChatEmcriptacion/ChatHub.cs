using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ChatEmcriptacion;
using Microsoft.AspNet.SignalR;
using ChatEmcriptacion.Models;

namespace ChatEmcriptacion
{
    public class ChatHub : Hub
    {
        public void Send(string key)
        {


            using (var db = new chatEntities())
            {
                string keydecript = "";
                encriptacion en = new encriptacion();
                if (key == "actualizacion")
                {
                    keydecript = "#$%&/!°678gds*´{ñ+";

                }
                else
                {

                    keydecript = en.DecriptacionRSA(key);
                }
                IEnumerable<mensajeria> listamessages = db.mensajeria.Include(m => m.usuario);

                IEnumerable<usuario> listausu = db.usuario.ToList();

                foreach (mensajeria item in listamessages)
                {
                    item.mensaje = en.Decript3DES(item.mensaje, keydecript);


                }



                Clients.All.SendChat(listamessages, listausu);
            }
        }



    }
}