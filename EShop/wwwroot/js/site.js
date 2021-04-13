let page=1;
let totalPages;
let url;

function initPagination(u,t) {
    totalPages = t;
    url = u;
}

$(window).scroll(async function () {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
        page++;

        if (page > totalPages)
            return;

        let response = await fetch(`${url}?page=${page}`);
        let html = await response.text();
        $('#IndexPageCards').append(html);
        console.log(html);
    }
});