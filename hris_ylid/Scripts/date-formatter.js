var monthNames = [
    "Januari", "Februari", "Maret", "April", "Mei", "Juni",
    "Juli", "Agustus", "September", "Oktober", "November", "Desember"
];

function looksLikeDate(value) {
    if (typeof value !== 'string') return false;
    return /^\d{1,4}\/\d{1,2}\/\d{1,4}(\s+\d{1,2}:\d{2}(:\d{2})?)?$/.test(value.trim());
}

function formatDateDisplay(value) {
    if (!looksLikeDate(value)) return value;

    var trimmed = value.trim();
    var spaceIdx = trimmed.indexOf(' ');
    var datePart = spaceIdx > -1 ? trimmed.substring(0, spaceIdx) : trimmed;
    var timePart = spaceIdx > -1 ? trimmed.substring(spaceIdx + 1) : '';

    var segs = datePart.split('/');
    if (segs.length < 3) return value;

    var year, month, day;

    if (segs[0].length === 4) {
        year = parseInt(segs[0], 10);
        month = parseInt(segs[1], 10);
        day = parseInt(segs[2], 10);
    } else if (segs[2].length === 4) {
        day = parseInt(segs[0], 10);
        month = parseInt(segs[1], 10);
        year = parseInt(segs[2], 10);
    } else {
        return value;
    }

    if (month < 1 || month > 12 || day < 1 || day > 31) return value;
    if (year <= 1900) return '';

    var result = day + ' ' + monthNames[month - 1] + ' ' + year;
    if (timePart) result += ' ' + timePart;

    return result;
}

// helper reusable untuk dipanggil di columnDefs tiap DataTable
function dateFormatRender(data, type, row) {
    if (type === 'display') {
        return formatDateDisplay(data);
    }
    return data;
}