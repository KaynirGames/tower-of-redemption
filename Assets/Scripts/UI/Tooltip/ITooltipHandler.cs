public interface ITooltipHandler
{
    bool OnTooltipRequest(out string content, out string header);
}
