// Получение всех песен
async function GetSong() {
    // отправляет запрос и получаем ответ
    const response = await fetch("/api/songs", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    // если запрос прошел нормально

    if (response.ok === true) {
        // получаем данные
        const songs = await response.json();
        let rows = document.querySelector("tbody");
        songs.forEach(song => {
            // добавляем полученные элементы в таблицу
            rows.append(row(song));
        });
    }
}
// Получение одной песни
async function GetSongById(id) {
    const response = await fetch("/api/songs/" + id, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const song = await response.json();
        const form = document.forms["songForm"];
        form.elements["id"].value = song.id;
        form.elements["name"].value = song.name;
        form.elements["singer"].value = song.singer;
        form.elements["auditions"].value = song.auditions;
    }
}

async function CreateSong(songName, songSinger, songAuditions) {
    const response = await fetch("api/songs", {
        method: "POST",
        headers: {
            "Accept": "application/json", "Content-Type":
                "application/json"
        },
        body: JSON.stringify({
            name: songName,
            singer: songSinger,
            auditions: songAuditions
        })
    });
    if (response.ok === true) {
        const song = await response.json();
        reset();
        document.querySelector("tbody").append(row(song));
    }
}

async function EditSong(songId, songName, songSinger, songAuditions) {
    const response = await fetch("/api/songs/" + songId, {
        method: "PUT",
        headers: {
            "Accept": "application/json", "Content-Type":
                "application/json"
        },
        body: JSON.stringify({
            id: parseInt(songId, 10),
            name: songName,
            singer: songSinger,
            auditions: songAuditions
        })
    });
    if (response.ok === true) {
        const song = await response.json();
        reset();
        document.querySelector("tr[data-rowid='" + song.id + "']").replaceWith(row(song));
    }
}
// Удаление песни
async function DeleteSong(id) {
    const response = await fetch("/api/songs/" + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const song = await response.json();
        document.querySelector("tr[data-rowid='" + song.id + "']").remove();
    }
}
// сброс формы
function reset() {
    const form = document.forms["songForm"];
    form.reset();
    form.elements["id"].value = 0;
}
// создание строки для таблицы
function row(song) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", song.id);
    const idTd = document.createElement("td");
    idTd.append(song.id);
    tr.append(idTd);

    const nameTd = document.createElement("td");
    nameTd.append(song.name);
    tr.append(nameTd);

    const singerTd = document.createElement("td");
    singerTd.append(song.singer);
    tr.append(singerTd);

    const auditionsTd = document.createElement("td");
    auditionsTd.append(song.auditions);
    tr.append(auditionsTd);

    const linksTd = document.createElement("td");
    const editLink = document.createElement("a");
    editLink.setAttribute("data-id", song.id);
    editLink.setAttribute("style", "cursor:pointer;padding:px;");
    editLink.append("Изменить");
    editLink.addEventListener("click", e => {
        e.preventDefault();
        GetSongById(song.id);
    });
    linksTd.append(editLink);
    const removeLink = document.createElement("a");
    removeLink.setAttribute("data-id", song.id);
    removeLink.setAttribute("style", "cursor:pointer;padding:px;");
    removeLink.append("Удалить");
    removeLink.addEventListener("click", e => {
        e.preventDefault();
        DeleteSong(song.id);
    });
    linksTd.append(removeLink);
    tr.appendChild(linksTd);
    return tr;
}

function InitialFunction() {
    // сброс значений формы
    document.getElementById("reset").click(function (e) {
        e.preventDefault();
        reset();
    })
    // отправка формы
    document.forms["songForm"].addEventListener("submit", e => {
        e.preventDefault();
        const form = document.forms["songForm"];
        const id = form.elements["id"].value;
        const name = form.elements["name"].value;
        const singer = form.elements["singer"].value;
        const auditions = form.elements["auditions"].value;
        if (id == 0)
            CreateSong(name, singer, auditions);
        else
            EditSong(id, name, singer, auditions);
    });
    GetSong();
}