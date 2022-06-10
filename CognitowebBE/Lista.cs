using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CognitowebBE
{
    public class Lista
    {
        public Formulario ObtieneFormulario(string id)
        {
            List<Formulario> listaFormulario = new List<Formulario>();
            listaFormulario = (List<Formulario>)(System.Web.HttpContext.Current.Session["listaFormulario"]);
            Formulario f = new Formulario();
            
            if (listaFormulario == null)
            {
                f = (Formulario)(System.Web.HttpContext.Current.Session["formulario"]);
                GrabaFormulario(f);
                return f;
            }
            else
            {
                var index = listaFormulario.FindIndex(formulario => formulario.id == id);

                if (index >= 0)
                {
                    return listaFormulario[index];
                }
                else
                {
                    f = (Formulario)(System.Web.HttpContext.Current.Session["formulario"]);
                    GrabaFormulario(f);
                    return f;
                }
            }
        }

        public bool GrabaFormulario(Formulario f)
        {
            System.Web.HttpContext.Current.Session.Add("formulario", f);
           
            List<Formulario> listaFormulario = new List<Formulario>();
            listaFormulario = (List<Formulario>)(System.Web.HttpContext.Current.Session["listaFormulario"]);

            if (listaFormulario == null)
            {
                List<Formulario> lF = new List<Formulario>();
                lF.Add(f);
                System.Web.HttpContext.Current.Session.Add("listaFormulario", lF);
            }
            else
            {
                var index = listaFormulario.FindIndex(formulario => formulario.id == f.id);

                if (index >= 0)
                {
                    listaFormulario[index] = f;
                }
                else
                {
                    listaFormulario.Add(f);
                }

                System.Web.HttpContext.Current.Session.Add("listaFormulario", listaFormulario);
            }
            
            return true;
        }

        public Cliente ObtieneCliente(string id)
        {
            List<Cliente> listaCliente = new List<Cliente>();
            listaCliente = (List<Cliente>)(System.Web.HttpContext.Current.Session["listaCliente"]);
            Cliente c = new Cliente();

            if (listaCliente == null)
            {
                c = (Cliente)(System.Web.HttpContext.Current.Session["cliente"]);
                GrabaCliente(c);
                return c;
            }
            else
            {
                var index = listaCliente.FindIndex(cliente => cliente.id == id);

                if (index >= 0)
                {
                    return listaCliente[index];
                }
                else
                {
                    c = (Cliente)(System.Web.HttpContext.Current.Session["cliente"]);
                    GrabaCliente(c);
                    return c;
                }
            }
        }

        public bool GrabaCliente(Cliente c)
        {
            System.Web.HttpContext.Current.Session.Add("cliente", c);

            List<Cliente> listaCliente = new List<Cliente>();
            listaCliente = (List<Cliente>)(System.Web.HttpContext.Current.Session["listaCliente"]);

            if (listaCliente == null)
            {
                List<Cliente> lC = new List<Cliente>();
                lC.Add(c);
                System.Web.HttpContext.Current.Session.Add("listaCliente", lC);
            }
            else
            {
                var index = listaCliente.FindIndex(cliente => cliente.id == c.id);

                if (index >= 0)
                {
                    listaCliente[index] = c;
                }
                else
                {
                    listaCliente.Add(c);
                }

                System.Web.HttpContext.Current.Session.Add("listaCliente", listaCliente);
            }

            return true;
        }

        public Circuito ObtieneCircuito(string id)
        {
            List<Circuito> listaCircuito = new List<Circuito>();
            listaCircuito = (List<Circuito>)(System.Web.HttpContext.Current.Session["listaCircuito"]);
            Circuito ci = new Circuito();

            if (listaCircuito == null)
            {
                ci = (Circuito)(System.Web.HttpContext.Current.Session["circuito"]);
                GrabaCircuito(ci);
                return ci;
            }
            else
            {
                var index = listaCircuito.FindIndex(circuito => circuito.id == id);

                if (index >= 0)
                {
                    return listaCircuito[index];
                }
                else
                {
                    ci = (Circuito)(System.Web.HttpContext.Current.Session["circuito"]);
                    GrabaCircuito(ci);
                    return ci;
                }
            }
        }

        public bool GrabaCircuito(Circuito ci)
        {
            System.Web.HttpContext.Current.Session.Add("circuito", ci);

            List<Circuito> listaCircuito = new List<Circuito>();
            listaCircuito = (List<Circuito>)(System.Web.HttpContext.Current.Session["listaCircuitoo"]);

            if (listaCircuito == null)
            {
                List<Circuito> lCi = new List<Circuito>();
                lCi.Add(ci);
                System.Web.HttpContext.Current.Session.Add("listaCircuito", lCi);
            }
            else
            {
                var index = listaCircuito.FindIndex(circuito => circuito.id == ci.id);

                if (index >= 0)
                {
                    listaCircuito[index] = ci;
                }
                else
                {
                    listaCircuito.Add(ci);
                }

                System.Web.HttpContext.Current.Session.Add("listaCircuito", listaCircuito);
            }

            return true;
        }
    }
}
