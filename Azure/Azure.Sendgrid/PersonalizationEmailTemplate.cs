using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.Sendgrid
{
    class SendgridEmailTemplateSend<T>
    {
        public SendgridEmailTemplateSend()
        {
            this.Personalizations = new List<Personalization<T>>();
        }
        public List<Personalization<T>> Personalizations { get; set; }
        public From From { get; set; }
        public string Template_id { get; set; }
    }
    class To
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    class Personalization<T>
    {
        public List<To> To { get; set; }
        public T Dynamic_template_data { get; set; }
        public string Subject { get; set; }
    }

    class From
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
