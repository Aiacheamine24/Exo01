using Exo01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exo01.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string namefilter, string typefilter, string fromPrice, string toPrice)
        {
            List<Equipement> equipements = Equipement.getEquipments();
            if (!string.IsNullOrEmpty(namefilter))
            {
                equipements = equipements.Where(e => e.Nom.Contains(namefilter)).ToList();
            }
            if (!string.IsNullOrEmpty(typefilter) && typefilter != "Choisir")
            {
                equipements = equipements.Where(e => e.Type.Contains(typefilter)).ToList();
            }
            if (!string.IsNullOrEmpty(fromPrice))
            {
                equipements = equipements.Where(e => e.Prix >= decimal.Parse(fromPrice)).ToList();
            }
            if (!string.IsNullOrEmpty(toPrice))
            {
                equipements = equipements.Where(e => e.Prix <= decimal.Parse(toPrice)).ToList();
            }
            return View(equipements);
        }

        // GET: Home/Add
        public ActionResult AddEquipment()
        {
            Equipement e = new Equipement();
            return View(e);
        }
        // GET: Home/Modify
        public ActionResult ModifyEquipment(int sn)
        {
            Equipement equipement = Equipement.getEquipment(sn);

            if (equipement != null)
            {
                ViewBag.Equipment = equipement;
                return View(equipement);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // Post: Home/Add
        [HttpPost]
        public ActionResult AddEquipment(string name, string type, string description, string price)
        {
            Equipement equipement = new Equipement
            {
                SN = 0,
                Nom = name,
                Type = type,
                Description = description,
                Prix = decimal.Parse(price)
            };

            if (ModelState.IsValid)
            {
                bool res = Equipement.addEquipment(equipement);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erreur lors de l'ajout de l'équipement";
                    return View(equipement);
                }
            }
            else
            {
                ViewBag.Error = "Erreur lors de l'ajout de l'équipement";
                return View(equipement);
            }
        }
        // Post: Home/Delete
        [HttpPost]
        public ActionResult DeleteEquipment(int sn)
        {
            bool res = Equipement.deleteEquipment(sn);
            if (res)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Erreur lors de la suppression de l'équipement";
                return RedirectToAction("Index");
            }
        }
        // Post: Home/Modify
        [HttpPost]
        public ActionResult ModifyEquipment(int sn, string name, string type, string description, string price)
        {
            Equipement equipement = new Equipement
            {
                SN = sn,
                Nom = name,
                Type = type,
                Description = description,
                Prix = decimal.Parse(price)
            };

            if (ModelState.IsValid)
            {
                bool res = Equipement.modifyEquipment(equipement);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Erreur lors de la modification de l'équipement";
                    return View(equipement);
                }
            }
            else
            {
                ViewBag.Error = "Erreur lors de la modification de l'équipement";
                return View(equipement);
            }
        }
    }
}