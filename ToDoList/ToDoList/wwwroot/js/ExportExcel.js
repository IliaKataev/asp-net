window.saveAsExcel = (jsonData, filename) =>
{
    const data = JSON.parse(jsonData);
    const worksheet = XLSX.utils.json_to_sheet(data);

    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, "Задачи");

    const excelBuffer = XLSX.write(workbook, { booktype: "xlsx", type: "array" });

    const blob = new Blob([excelBuffer], { type: 'application/octet-stream' });

    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = filename;

    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}