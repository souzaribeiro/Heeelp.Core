using System.Collections.Generic;

namespace Heeelp.Core.Domain.ReadModel.DTO
{
    public class ExternalNotificationDTO
    {
        //para minimizar a chance de erro de utilizacao, ao inicializar a classe, passa-se todos os parametros obrigatorios no construtor. 
        //a classe tem a inteligencia de separar quem vai adiante como propriedade e quem cai como lista de parametros
        // nos casos em que for necessario parametros adicionais, utilizar o metodo AddParameter


        public int PersonFromId { get;  }

        public int PersonToId { get; }

        public string PersonToName { get; }

        public int LanguageId { get; }

        public string MessageCodeType { get; }

        public string Title { get;  }

        public string Body { get; }

        public string CustomClub { get;  }

        public string CustomHeeelpPersonDomain { get;  }

        public string Logotipo { get;  }

        public Dictionary<string, string> ListKeys { get;  }

        public ExternalNotificationDTO(int personFromId, int personToId, string personName, int languageId, string messageCodeType, string title, string body, string customClub, string customHeeelpPersonDomain, string logotipo)
        {
            this.PersonFromId = personFromId;
            this.PersonToId = personToId;
            this.PersonToName = personName;
            this.LanguageId = languageId;
            this.MessageCodeType = messageCodeType;
            this.Title = title;
            this.Body = body;
            this.CustomClub = customClub;
            this.CustomHeeelpPersonDomain = customHeeelpPersonDomain;
            this.Logotipo = logotipo;
            this.ListKeys = new Dictionary<string, string>();

            ListKeys.Add("NomePessoa", this.PersonToName);
            ListKeys.Add("LogoTipo", this.Logotipo);
            ListKeys.Add("NomeClubeBeneficio", this.CustomClub);
            ListKeys.Add("URLCustomizada", this.CustomHeeelpPersonDomain);
            ListKeys.Add("MC:SUBJECT", title);
            ListKeys.Add("Mensagem", body);
        }

        public void AddParameter(string KeyName, string Value)
        {
            ListKeys.Add(KeyName, Value);
        }

    }

}
