

using System.Text;
using System.Text.Json;
using PocGPT.Core.InterfaceService;
using PocGPT.Core.Model;
using Azure;
using Azure.AI.OpenAI;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;

namespace PocGPT.Core
{
    public class TicketService : ITicketsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        

        public TicketService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;

        }

        public async Task<ChatCompletions> GetResultChatGpt(List<Messages> messages)
        {
            var uriAzureApi = _configuration.GetSection("GPT:URI");
            var key1 = _configuration.GetSection("GPT:Key1");
            var key2 = _configuration.GetSection("GPT:Key2");
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            System.Net.ServicePointManager.Expect100Continue = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.DefaultConnectionLimit = 9999;
            var handler = new HttpClientHandler
            {
                SslProtocols = SslProtocols.Tls12 // o la versión de TLS que desees utilizar  
            };
            var httpClient = new HttpClient(handler);

            OpenAIClient client = new OpenAIClient(
                new Uri(uriAzureApi.Value),
                new AzureKeyCredential(key1.Value));

            // ### If streaming is selected
            Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(
                deploymentOrModelName: "gpt-4-32k",
                new ChatCompletionsOptions()
                {
                    Messages =
                    {
            new ChatMessage(ChatRole.System, @"You are AI Assistant to create chatbots using this as example: ""
{""flow"":{""onboarding"":{""$contentActions"":[{""input"":{""bypass"":false,""$cardContent"":{""document"":{""id"":""a9f6665c-2e05-489f-a72a-63acf23b3131"",""type"":""text/plain""},""editable"":false,""deletable"":false,""position"":""right"",""editing"":false}}}],""$conditionOutputs"":[{""stateId"":""welcome"",""conditions"":[{""source"":""input"",""comparison"":""matches"",""values"":["".*""]}],""$id"":""8ec691ec-8c54-4302-9ce1-e5549422b674"",""$connId"":""con_3""}],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""fallback""},""$tags"":[],""id"":""onboarding"",""root"":true,""$title"":""Start"",""$position"":{""top"":""120px"",""left"":""644px""}},""fallback"":{""$contentActions"":[{""input"":{""bypass"":true,""$cardContent"":{""document"":{""id"":""27582a94-06eb-4590-8c5e-4af48e7437bf"",""type"":""text/plain""},""editable"":false,""deletable"":true,""position"":""right"",""editing"":false}}}],""$conditionOutputs"":[{""stateId"":""error"",""conditions"":[{""source"":""input"",""comparison"":""matches"",""values"":["".*""]}],""$id"":""73e56bf6-1a7e-4379-a178-8642d147819e"",""$connId"":""con_8""}],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""onboarding""},""$tags"":[],""id"":""fallback"",""$title"":""Exceptions"",""$position"":{""top"":""120px"",""left"":""877px""}},""welcome"":{""$contentActions"":[{""action"":{""$id"":""1bd11da5-2f60-4135-9b2b-f623dbe3a795"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000000"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000000"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}},""$invalid"":false,""$$hashKey"":""object:1066""},{""action"":{""$id"":""5700d09a-44a4-4de0-8702-fbdb7bd5ec59"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000001"",""type"":""text/plain"",""content"":""Olá! {{contact.name}}!\nSeja bem-vindo(a)!""},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000001"",""type"":""text/plain"",""content"":""Olá! {{contact.name}}!\nSeja bem-vindo(a)!""},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}},""$invalid"":false,""$$hashKey"":""object:1067""},{""input"":{""bypass"":false,""$cardContent"":{""document"":{""id"":""0e67f63d-8811-4314-bdb3-df64c0de3ab6"",""type"":""text/plain"",""content"":""User input""},""editable"":false,""deletable"":true,""position"":""right"",""editing"":false},""$invalid"":false},""$invalid"":false,""$$hashKey"":""object:1068""}],""$conditionOutputs"":[],""$enteringCustomActions"":[{""$id"":""08b53ba4-c7da-446c-bfa9-571f7101efd3"",""$typeOfContent"":"""",""type"":""TrackEvent"",""$title"":""Event tracking"",""$invalid"":false,""settings"":{""extras"":{},""category"":""event"",""action"":""test""},""conditions"":[]},{""$id"":""c3899fa0-7b6c-427e-8e05-cc38dab2617d"",""$typeOfContent"":"""",""type"":""MergeContact"",""$title"":""Set contact"",""$invalid"":false,""settings"":{""extras"":{}},""conditions"":[]},{""$id"":""aeee3b11-36cc-494f-b1df-75b7b4870da0"",""$typeOfContent"":"""",""type"":""ExecuteScript"",""$title"":""Execute script"",""$invalid"":true,""$validationError"":{},""settings"":{""function"":""run"",""source"":""/**\n* All input variables needs to be passed as function param;\n* Objects received as param needs to be parsed. Ex.: JSON.parse(inputVariable1);\n* Objects returned needs to be stringfied. Ex.: JSON.stringify(inputVariable1);\n**/\nfunction run(inputVariable1, inputVariable2) {\n\treturn \""Hello BLiP\""; //Return value will be saved as \""Return value variable\"" field name\n}"",""inputVariables"":[""bucket.?"",""resource.?"",""application.identifier"",""application.domain"",""application.instance""]},""conditions"":[]}],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""fallback"",""$invalid"":false},""$tags"":[{""background"":""#2cc3d5"",""label"":""Script"",""id"":""blip-tag-849d7f6a-1c12-6f05-a2ce-1d184ec69210""}],""id"":""welcome"",""$title"":""Welcome"",""$position"":{""top"":""240px"",""left"":""644px""},""$invalidContentActions"":false,""$invalidOutputs"":false,""$invalidCustomActions"":true,""$invalid"":true},""error"":{""$contentActions"":[{""action"":{""$id"":""9db7041c-f90a-4a87-bac7-35c1a4d87573"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000002"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000002"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}}},{""action"":{""$id"":""15bb43ec-511c-45a0-a114-aa973a75b7d9"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000003"",""type"":""text/plain"",""content"":""Desculpe, não consegui entender!""},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000003"",""type"":""text/plain"",""content"":""Desculpe, não consegui entender!""},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}}},{""input"":{""bypass"":true,""$cardContent"":{""document"":{""id"":""7008dc82-45bd-4692-a048-3250455ccdc1"",""type"":""text/plain""},""editable"":false,""deletable"":true,""position"":""right"",""editing"":false}}}],""$conditionOutputs"":[],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""onboarding""},""$tags"":[],""id"":""error"",""$title"":""Default error"",""$position"":{""top"":""240px"",""left"":""877px""}},""desk:ca5bf9b1-ac9e-4f87-822f-e6bc83e076fe"":{""$contentActions"":[{""input"":{""bypass"":false,""conditions"":[{""source"":""context"",""variable"":""desk_forwardToDeskState_status"",""comparison"":""equals"",""values"":[""Success""]}],""$cardContent"":{""document"":{""id"":""4ff9e8a9-6af4-439e-bf16-a06129216243"",""type"":""text/plain"",""textContent"":""User input"",""content"":""User input""},""editable"":false,""deletable"":false,""position"":""right"",""editing"":false},""$invalid"":false},""$invalid"":false,""$$hashKey"":""object:797""}],""$conditionOutputs"":[{""$isDeskOutput"":true,""conditions"":[{""source"":""context"",""variable"":""input.type"",""comparison"":""equals"",""values"":[""application/vnd.iris.ticket+json""]},{""source"":""context"",""variable"":""input.content@status"",""comparison"":""equals"",""values"":[""ClosedAttendant""]}],""$invalid"":false,""$$hashKey"":""object:817"",""stateId"":""error"",""$connId"":""con_41"",""$id"":""1586ae71-6eb0-405c-bf5d-b14cfafc13e4""},{""$isDeskOutput"":true,""conditions"":[{""source"":""context"",""variable"":""input.type"",""comparison"":""equals"",""values"":[""application/vnd.iris.ticket+json""]},{""source"":""context"",""variable"":""input.content@status"",""comparison"":""equals"",""values"":[""ClosedClient""]}],""$invalid"":false,""$$hashKey"":""object:818"",""stateId"":""onboarding"",""$connId"":""con_56"",""$id"":""6434fff1-5fa2-4c61-9d1f-7d3b2b133ded""},{""$isDeskOutput"":true,""conditions"":[{""source"":""context"",""variable"":""input.type"",""comparison"":""equals"",""values"":[""application/vnd.iris.ticket+json""]},{""source"":""context"",""variable"":""input.content@status"",""comparison"":""equals"",""values"":[""ClosedClientInactivity""]}],""$invalid"":false,""$$hashKey"":""object:819"",""stateId"":""onboarding"",""$connId"":""con_46"",""$id"":""7b8580db-26bd-4b1d-855c-a381a7015481""},{""$isDeskOutput"":true,""$isDeskDefaultOutput"":true,""conditions"":[{""source"":""context"",""variable"":""desk_forwardToDeskState_status"",""comparison"":""equals"",""values"":[""Error""]}],""stateId"":""fallback"",""$invalid"":false,""$$hashKey"":""object:820""}],""$enteringCustomActions"":[{""$id"":""8b20596f-6fa5-4794-bdbf-5de7f942540b"",""$typeOfContent"":"""",""type"":""ForwardToDesk"",""conditions"":[],""settings"":{},""$invalid"":false}],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""desk:ca5bf9b1-ac9e-4f87-822f-e6bc83e076fe"",""$invalid"":false},""$tags"":[],""id"":""desk:ca5bf9b1-ac9e-4f87-822f-e6bc83e076fe"",""deskStateVersion"":""3.0.0"",""root"":false,""$title"":""Customer service"",""$afterStateChangedActions"":[{""$id"":""6c2f43b9-85c3-4ae3-8db8-d62f4def243e"",""$typeOfContent"":"""",""type"":""LeavingFromDesk"",""conditions"":[],""settings"":{}}],""$position"":{""top"":""195px"",""left"":""140px""},""$invalidContentActions"":false,""$invalidOutputs"":false,""$invalidCustomActions"":false,""$invalid"":false}},""globalActions"":{""$contentActions"":[],""$conditionOutputs"":[],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""fallback""},""$tags"":[],""id"":""global-actions""}}"),
            new ChatMessage(ChatRole.User, @"Faça um chatbot de venda de carnes")
                    },
                    Temperature = (float)0.7,
                    MaxTokens = 32768,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });
            using StreamingChatCompletions streamingChatCompletions = response.Value;


            // ### If streaming is not selected
            Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync(
                "gpt-4-32k",
                new ChatCompletionsOptions()
                {
                    Messages =
                    {
                        new ChatMessage(ChatRole.System, @"You are AI Assistant to create chatbots using this as example: ""
{""flow"":{""onboarding"":{""$contentActions"":[{""input"":{""bypass"":false,""$cardContent"":{""document"":{""id"":""a9f6665c-2e05-489f-a72a-63acf23b3131"",""type"":""text/plain""},""editable"":false,""deletable"":false,""position"":""right"",""editing"":false}}}],""$conditionOutputs"":[{""stateId"":""welcome"",""conditions"":[{""source"":""input"",""comparison"":""matches"",""values"":["".*""]}],""$id"":""8ec691ec-8c54-4302-9ce1-e5549422b674"",""$connId"":""con_3""}],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""fallback""},""$tags"":[],""id"":""onboarding"",""root"":true,""$title"":""Start"",""$position"":{""top"":""120px"",""left"":""644px""}},""fallback"":{""$contentActions"":[{""input"":{""bypass"":true,""$cardContent"":{""document"":{""id"":""27582a94-06eb-4590-8c5e-4af48e7437bf"",""type"":""text/plain""},""editable"":false,""deletable"":true,""position"":""right"",""editing"":false}}}],""$conditionOutputs"":[{""stateId"":""error"",""conditions"":[{""source"":""input"",""comparison"":""matches"",""values"":["".*""]}],""$id"":""73e56bf6-1a7e-4379-a178-8642d147819e"",""$connId"":""con_8""}],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""onboarding""},""$tags"":[],""id"":""fallback"",""$title"":""Exceptions"",""$position"":{""top"":""120px"",""left"":""877px""}},""welcome"":{""$contentActions"":[{""action"":{""$id"":""1bd11da5-2f60-4135-9b2b-f623dbe3a795"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000000"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000000"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}},""$invalid"":false,""$$hashKey"":""object:1066""},{""action"":{""$id"":""5700d09a-44a4-4de0-8702-fbdb7bd5ec59"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000001"",""type"":""text/plain"",""content"":""Olá! {{contact.name}}!\nSeja bem-vindo(a)!""},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000001"",""type"":""text/plain"",""content"":""Olá! {{contact.name}}!\nSeja bem-vindo(a)!""},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}},""$invalid"":false,""$$hashKey"":""object:1067""},{""input"":{""bypass"":false,""$cardContent"":{""document"":{""id"":""0e67f63d-8811-4314-bdb3-df64c0de3ab6"",""type"":""text/plain"",""content"":""User input""},""editable"":false,""deletable"":true,""position"":""right"",""editing"":false},""$invalid"":false},""$invalid"":false,""$$hashKey"":""object:1068""}],""$conditionOutputs"":[],""$enteringCustomActions"":[{""$id"":""08b53ba4-c7da-446c-bfa9-571f7101efd3"",""$typeOfContent"":"""",""type"":""TrackEvent"",""$title"":""Event tracking"",""$invalid"":false,""settings"":{""extras"":{},""category"":""event"",""action"":""test""},""conditions"":[]},{""$id"":""c3899fa0-7b6c-427e-8e05-cc38dab2617d"",""$typeOfContent"":"""",""type"":""MergeContact"",""$title"":""Set contact"",""$invalid"":false,""settings"":{""extras"":{}},""conditions"":[]},{""$id"":""aeee3b11-36cc-494f-b1df-75b7b4870da0"",""$typeOfContent"":"""",""type"":""ExecuteScript"",""$title"":""Execute script"",""$invalid"":true,""$validationError"":{},""settings"":{""function"":""run"",""source"":""/**\n* All input variables needs to be passed as function param;\n* Objects received as param needs to be parsed. Ex.: JSON.parse(inputVariable1);\n* Objects returned needs to be stringfied. Ex.: JSON.stringify(inputVariable1);\n**/\nfunction run(inputVariable1, inputVariable2) {\n\treturn \""Hello BLiP\""; //Return value will be saved as \""Return value variable\"" field name\n}"",""inputVariables"":[""bucket.?"",""resource.?"",""application.identifier"",""application.domain"",""application.instance""]},""conditions"":[]}],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""fallback"",""$invalid"":false},""$tags"":[{""background"":""#2cc3d5"",""label"":""Script"",""id"":""blip-tag-849d7f6a-1c12-6f05-a2ce-1d184ec69210""}],""id"":""welcome"",""$title"":""Welcome"",""$position"":{""top"":""240px"",""left"":""644px""},""$invalidContentActions"":false,""$invalidOutputs"":false,""$invalidCustomActions"":true,""$invalid"":true},""error"":{""$contentActions"":[{""action"":{""$id"":""9db7041c-f90a-4a87-bac7-35c1a4d87573"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000002"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000002"",""type"":""application/vnd.lime.chatstate+json"",""content"":{""state"":""composing"",""interval"":1000}},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}}},{""action"":{""$id"":""15bb43ec-511c-45a0-a114-aa973a75b7d9"",""$typeOfContent"":"""",""type"":""SendMessage"",""settings"":{""id"":""00000000-0000-0000-0000-000000000003"",""type"":""text/plain"",""content"":""Desculpe, não consegui entender!""},""$cardContent"":{""document"":{""id"":""00000000-0000-0000-0000-000000000003"",""type"":""text/plain"",""content"":""Desculpe, não consegui entender!""},""editable"":true,""deletable"":true,""position"":""left"",""editing"":false}}},{""input"":{""bypass"":true,""$cardContent"":{""document"":{""id"":""7008dc82-45bd-4692-a048-3250455ccdc1"",""type"":""text/plain""},""editable"":false,""deletable"":true,""position"":""right"",""editing"":false}}}],""$conditionOutputs"":[],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""onboarding""},""$tags"":[],""id"":""error"",""$title"":""Default error"",""$position"":{""top"":""240px"",""left"":""877px""}},""desk:ca5bf9b1-ac9e-4f87-822f-e6bc83e076fe"":{""$contentActions"":[{""input"":{""bypass"":false,""conditions"":[{""source"":""context"",""variable"":""desk_forwardToDeskState_status"",""comparison"":""equals"",""values"":[""Success""]}],""$cardContent"":{""document"":{""id"":""4ff9e8a9-6af4-439e-bf16-a06129216243"",""type"":""text/plain"",""textContent"":""User input"",""content"":""User input""},""editable"":false,""deletable"":false,""position"":""right"",""editing"":false},""$invalid"":false},""$invalid"":false,""$$hashKey"":""object:797""}],""$conditionOutputs"":[{""$isDeskOutput"":true,""conditions"":[{""source"":""context"",""variable"":""input.type"",""comparison"":""equals"",""values"":[""application/vnd.iris.ticket+json""]},{""source"":""context"",""variable"":""input.content@status"",""comparison"":""equals"",""values"":[""ClosedAttendant""]}],""$invalid"":false,""$$hashKey"":""object:817"",""stateId"":""error"",""$connId"":""con_41"",""$id"":""1586ae71-6eb0-405c-bf5d-b14cfafc13e4""},{""$isDeskOutput"":true,""conditions"":[{""source"":""context"",""variable"":""input.type"",""comparison"":""equals"",""values"":[""application/vnd.iris.ticket+json""]},{""source"":""context"",""variable"":""input.content@status"",""comparison"":""equals"",""values"":[""ClosedClient""]}],""$invalid"":false,""$$hashKey"":""object:818"",""stateId"":""onboarding"",""$connId"":""con_56"",""$id"":""6434fff1-5fa2-4c61-9d1f-7d3b2b133ded""},{""$isDeskOutput"":true,""conditions"":[{""source"":""context"",""variable"":""input.type"",""comparison"":""equals"",""values"":[""application/vnd.iris.ticket+json""]},{""source"":""context"",""variable"":""input.content@status"",""comparison"":""equals"",""values"":[""ClosedClientInactivity""]}],""$invalid"":false,""$$hashKey"":""object:819"",""stateId"":""onboarding"",""$connId"":""con_46"",""$id"":""7b8580db-26bd-4b1d-855c-a381a7015481""},{""$isDeskOutput"":true,""$isDeskDefaultOutput"":true,""conditions"":[{""source"":""context"",""variable"":""desk_forwardToDeskState_status"",""comparison"":""equals"",""values"":[""Error""]}],""stateId"":""fallback"",""$invalid"":false,""$$hashKey"":""object:820""}],""$enteringCustomActions"":[{""$id"":""8b20596f-6fa5-4794-bdbf-5de7f942540b"",""$typeOfContent"":"""",""type"":""ForwardToDesk"",""conditions"":[],""settings"":{},""$invalid"":false}],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""desk:ca5bf9b1-ac9e-4f87-822f-e6bc83e076fe"",""$invalid"":false},""$tags"":[],""id"":""desk:ca5bf9b1-ac9e-4f87-822f-e6bc83e076fe"",""deskStateVersion"":""3.0.0"",""root"":false,""$title"":""Customer service"",""$afterStateChangedActions"":[{""$id"":""6c2f43b9-85c3-4ae3-8db8-d62f4def243e"",""$typeOfContent"":"""",""type"":""LeavingFromDesk"",""conditions"":[],""settings"":{}}],""$position"":{""top"":""195px"",""left"":""140px""},""$invalidContentActions"":false,""$invalidOutputs"":false,""$invalidCustomActions"":false,""$invalid"":false}},""globalActions"":{""$contentActions"":[],""$conditionOutputs"":[],""$enteringCustomActions"":[],""$leavingCustomActions"":[],""$inputSuggestions"":[],""$defaultOutput"":{""stateId"":""fallback""},""$tags"":[],""id"":""global-actions""}}"),
            new ChatMessage(ChatRole.User, @"Faça um chatbot de venda de carnes")
                    },
                    Temperature = (float)0.7,
                    MaxTokens = 32768,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

            ChatCompletions completions = responseWithoutStream.Value;






            //Completions completions = completionsResponse.Value;
            return completions;
        }

        public async Task<IEnumerable<Messages>> GetTickeMessages(Guid IdDoTicket)
        {
            //var postPrompt = "";
            //var teste = await this.GetResultChatGpt(postPrompt);
            var tokenApi = _configuration.GetSection("API:Authorization");
            var baseAddress = _configuration.GetSection("API:UriCommands");

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress.Value);
            var routeResult = $"{baseAddress.Value}/tickets/{IdDoTicket}/messages?$take=100&direction=desc";

            var postData = new
            {
              id = Guid.NewGuid(),
              to= "postmaster@desk.msging.net",
              method= "get",
              uri= "/tickets/c6f82e88-f68d-4870-840f-01879ec113b9/messages?$take=100&direction=desc"
            };
            var jsonContent = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(postData),
            Encoding.UTF8,
            "application/json"
            );

            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseAddress.Value}");
            request.Headers.Add("Authorization", $"{tokenApi.Value}");

            var result = new List<Messages>();
            var response = await client.PostAsync($"{baseAddress.Value}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                /*result = JsonSerializer.Deserialize<List<Messages>>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });*/
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

        private String FormatJson(string dataJson)
        {
            return "";
        }

    }
}
