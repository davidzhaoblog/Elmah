1. should load this file in Index.cshtml if Popup/Inline Create/Edit enabled
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
2. Check why CRUD partial not generated. Need more consistant approach, persist?
3. 