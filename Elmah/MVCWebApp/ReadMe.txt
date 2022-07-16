1. Should have _DetailsSummary.cshtml(For Tiles) for database tables with a lot of columns, couldn't display all columns
1.1. should have an enum for _DetailsSummary.cshtml and a new setting column in UIViewSettings.
1.2. Usually, such database tables(with a lot of columns) should have a seperate page or popup for edit/create, which meansing _DetailsSummary.cshtml should enabled when Popup enabled.
1.3. For Html Table content of <tr>...</tr>, we can directly modify _ListDetailsItem.cshtml, because no-where else is using it.
2. Should have a _Tile.cshml, currently a Tile is a Bootstrap .Card, for one record, to add <div class="card">...</div> wrapper.
2.1. should use _DetailsSummary.cshtml in .card-body when available. Usually, CRUD Dialog is enabled.
2.2. use _Create/_Delete/_Details/_Edit.cshtml when inline-editing. _DetailsSummary.cshtml should not be enabled in this case.
3. Should have BatchDelete/BatchEdit in UIViewSettings
4. Almost pure CSS FloatingActionButton with labels 
4.1. https://codepen.io/fosmont/pen/oNbOQWd
4.2. https://mdbootstrap.com/snippets/jquery/vrivero/2265260
4.3. * https://www.coderglass.com/html/gmail-style-inbox-page-html.php
5. Image field(Url).
6. in *.Properties.csv
6.1. add PropertyKnownCategory: Name, which is the title of each record on UI. By default it is same as NameValuePair.Name definition. Use existing "Name" or similar columns as "PropertyKnownCategory.Name", otherwise use Name expression in NameValuePair.
6.2. add one "OnUISummary".


<label class="control-label" for="ResponseBody_0__Sequence">Sequence</label>
<label class="control-label" for="ResponseBody_0__TimeUtc">Sequence</label>
<input class="form-control" readonly="" type="number" data-val="true" data-val-required="The Sequence field is required." id="ResponseBody_0__Sequence" name="ResponseBody[0].Sequence" value="1">