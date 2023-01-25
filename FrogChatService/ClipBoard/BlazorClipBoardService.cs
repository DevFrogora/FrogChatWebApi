using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogChatService.ClipBoard
{
    public class BlazorClipBoardService : IClipboardService
    {
        private readonly IJSRuntime jSRuntime;

        public BlazorClipBoardService(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }
        public async Task CopyToClipboard(string text)
        {
            await jSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
    }
}
