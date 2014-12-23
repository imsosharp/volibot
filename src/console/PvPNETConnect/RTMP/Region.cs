#region

using System;

#endregion

namespace LoLLauncher
{
    public enum Region
    {
        [ServerValue("prod.na2.lol.riotgames.com")] [LoginQueueValue("https://lq.na2.lol.riotgames.com/")] [LocaleValue("en_US")] [UseGarenaValue(false)] Na,

        [ServerValue("prod.euw1.lol.riotgames.com")] [LoginQueueValue("https://lq.euw1.lol.riotgames.com/")] [LocaleValue("en_GB")] [UseGarenaValue(false)] Euw,

        [ServerValue("prod.eun1.lol.riotgames.com")] [LoginQueueValue("https://lq.eun1.lol.riotgames.com/")] [LocaleValue("en_GB")] [UseGarenaValue(false)] Eun,

        [ServerValue("prod.kr.lol.riotgames.com")] [LoginQueueValue("https://lq.kr.lol.riotgames.com/")] [LocaleValue("ko_KR")] [UseGarenaValue(false)] Kr,

        [ServerValue("prod.br.lol.riotgames.com")] [LoginQueueValue("https://lq.br.lol.riotgames.com/")] [LocaleValue("pt_BR")] [UseGarenaValue(false)] Br,

        [ServerValue("prod.tr.lol.riotgames.com")] [LoginQueueValue("https://lq.tr.lol.riotgames.com/")] [LocaleValue("pt_BR")] [UseGarenaValue(false)] Tr,

        [ServerValue("prod.ru.lol.riotgames.com")] [LoginQueueValue("https://lq.ru.lol.riotgames.com/")] [LocaleValue("en_US")] [UseGarenaValue(false)] Ru,

        [ServerValue("prod.pbe1.lol.riotgames.com")] [LoginQueueValue("https://lq.pbe1.lol.riotgames.com/")] [LocaleValue("en_US")] [UseGarenaValue(false)] Pbe,

        [ServerValue("prod.lol.garenanow.com")] [LoginQueueValue("https://lq.lol.garenanow.com/")] [LocaleValue("en_US")] [UseGarenaValue(true)] Sg,

        [ServerValue("prod.lol.garenanow.com")] [LoginQueueValue("https://lq.lol.garenanow.com/")] [LocaleValue("en_US")] [UseGarenaValue(true)] My,

        [ServerValue("prod.lol.garenanow.com")] [LoginQueueValue("https://lq.lol.garenanow.com/")] [LocaleValue("en_US")] [UseGarenaValue(true)] Sgmy,

        [ServerValue("prodtw.lol.garenanow.com")] [LoginQueueValue("https://loginqueuetw.lol.garenanow.com/")] [LocaleValue("en_US")] [UseGarenaValue(true)] Tw,

        [ServerValue("prodth.lol.garenanow.com")] [LoginQueueValue("https://lqth.lol.garenanow.com/")] [LocaleValue("en_US")] [UseGarenaValue(true)] Th,

        [ServerValue("prodph.lol.garenanow.com")] [LoginQueueValue("https://storeph.lol.garenanow.com/")] [LocaleValue("en_US")] [UseGarenaValue(true)] Ph,

        [ServerValue("prodvn.lol.garenanow.com")] [LoginQueueValue("https://lqvn.lol.garenanow.com/")] [LocaleValue("en_US")] [UseGarenaValue(true)] Vn,

        [ServerValue("prod.oc1.lol.riotgames.com")] [LoginQueueValue("https://lq.oc1.lol.riotgames.com/")] [LocaleValue("en_US")] [UseGarenaValue(false)] Oce,

        [ServerValue("prod.la1.lol.riotgames.com")] [LoginQueueValue("https://lq.la1.lol.riotgames.com/")] [LocaleValue("en_US")] [UseGarenaValue(false)] Lan,

        [ServerValue("prod.la2.lol.riotgames.com")] [LoginQueueValue("https://lq.la2.lol.riotgames.com/")] [LocaleValue("en_US")] [UseGarenaValue(false)] Las
    }

    public static class RegionInfo
    {
        public static string GetServerValue(Enum value)
        {
            string output = null;
            var type = value.GetType();

            var fi = type.GetField(value.ToString());
            var attrs =
                fi.GetCustomAttributes(typeof (ServerValue),
                    false) as ServerValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }

        public static string GetLoginQueueValue(Enum value)
        {
            string output = null;
            var type = value.GetType();

            var fi = type.GetField(value.ToString());
            var attrs =
                fi.GetCustomAttributes(typeof (LoginQueueValue),
                    false) as LoginQueueValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }

        public static string GetLocaleValue(Enum value)
        {
            string output = null;
            var type = value.GetType();

            var fi = type.GetField(value.ToString());
            var attrs =
                fi.GetCustomAttributes(typeof (LocaleValue),
                    false) as LocaleValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }

        public static bool GetUseGarenaValue(Enum value)
        {
            var output = false;
            var type = value.GetType();

            var fi = type.GetField(value.ToString());
            var attrs =
                fi.GetCustomAttributes(typeof (UseGarenaValue),
                    false) as UseGarenaValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }
    }

    public class ServerValue : Attribute
    {
        public ServerValue(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }

    public class LoginQueueValue : Attribute
    {
        public LoginQueueValue(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }

    public class LocaleValue : Attribute
    {
        public LocaleValue(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }

    public class UseGarenaValue : Attribute
    {
        public UseGarenaValue(bool value)
        {
            Value = value;
        }

        public bool Value { get; private set; }
    }
}