let books = []


async function loadBooks() {
    const res = await fetch("api/books");
    const books = await res.json();
    const list = document.getElementById("bookList");
    list.innerHTML = "";
    books.forEach(book => {
        const li = document.createElement("li");
        li.innerHTML = `<label>
            <input type="checkbox" value="${book.title}">
            ${book.title} - ${book.author}
            </label>`
        list.appendChild(li);
    })
}

async function makeOrder(event) {
    event.preventDefault();

    const selected = Array.from(document.querySelectorAll("#bookList input:checked")).map(input => input.value);

    if (selected.length === 0) {
        alert("Выберите хотя бы одну книгу!");
        return;
    }

    const res = await fetch("/api/order", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },

        body: JSON.stringify({ items: selected })
    });

    if (res.redirected) {
        window.location.href = res.url;
    }
    else {
        const message = await res.text();
        alert("Ответ от сервера" + message);
    }
}



async function showLastOrder() {
    const res = await fetch("/api/last-order");
    if (res.status === 404) {
        alert("Не было заказов");
        return;
    }

    const order = await res.json();
    alert("Ваш последний заказ: " + order.items.join(", "))
}


async function showAllOrders() {
    const res = await fetch("/api/orders");
    const orders = await res.json();

    if (res.length === 0) {
        alert("Не было заказов");
        return;
    }

    let message = "Все заказы: \n";
    orders.forEach((order, index) => {
        message += `${index + 1}) ${order.items.join(", ")}\n`
    })

    alert(message);

}