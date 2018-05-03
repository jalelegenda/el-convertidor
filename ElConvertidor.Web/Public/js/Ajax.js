export default function ajax(url, data, method, onSuccess, onError) {
    if(data === null){
        fetch(url, {
            credentials: 'include',
            method: method === null ? 'GET' : method
        })
        .then(onSuccess, onError);
    } else {
        fetch(url, {
            body: data,
            credentials: 'include',
            method: 'POST'
        })
        .then(onSuccess, onError);
    }
}