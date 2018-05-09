export default function() {
    const fetching = new Event('fetching', {'view': document, 'bubbles': true, 'cancelable': false});
    const fetchend = new Event('fetchend', {'view': document, 'bubbles': true, 'cancelable': false});

    let fetchCall = fetch.apply(this, arguments);

    document.dispatchEvent(fetching);

    fetchCall.then(() => {
        document.dispatchEvent(fetchend);
    }).catch(() => {
        document.dispatchEvent(fetchend);
    });

    return fetchCall;
}