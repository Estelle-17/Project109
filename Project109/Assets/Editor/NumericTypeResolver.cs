using System;
using System.Text.RegularExpressions;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

public class NumericTypeResolver : INodeTypeResolver
{
    // 정수 및 실수 판별을 위한 정규식
    private static readonly Regex IntegerRegex = new Regex(@"^-?([1-9][0-9]*|0)$");
    private static readonly Regex FloatRegex = new Regex(@"^-?([1-9][0-9]*|0)\.[0-9]+([eE][-+]?[0-9]+)?$");

    public bool Resolve(NodeEvent nodeEvent, ref Type currentType)
    {
        // 이미 타입이 지정되어 있거나 스칼라(단순 값)가 아니면 통과
        if (currentType != typeof(object) || !(nodeEvent is Scalar scalar))
            return false;

        // 따옴표로 감싸져 있지 않은 경우에만 타입 추론 실행
        if (scalar.Style == YamlDotNet.Core.ScalarStyle.Plain)
        {
            if (IntegerRegex.IsMatch(scalar.Value))
            {
                currentType = typeof(int);
                return true;
            }
            if (FloatRegex.IsMatch(scalar.Value))
            {
                currentType = typeof(float);
                return true;
            }
            if (scalar.Value == "true" || scalar.Value == "false")
            {
                currentType = typeof(bool);
                return true;
            }
        }

        return false;
    }
}
