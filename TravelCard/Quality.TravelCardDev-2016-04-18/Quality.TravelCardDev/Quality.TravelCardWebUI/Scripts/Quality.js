function alternateRows() {
el = document.getElementsByTagName("standard-table");
for (i = 0; i < el.length; i++)
if (el.className == "altRows") {
rows = el.getElementsByTagName("tr");
for (j = 0; j < rows.length; j++)
rows[j].className = "row" + (j % 2);
}
}

with
.row0 {
background: #ccc;
}
.row1 {
background: #eee;
}

