export default function ajax(url, data, onSuccess, onError) {
    if(data === null){
        fetch(url, {
            credentials: 'include',
            method: 'GET'
        })
    } else {
        fetch(url, {
            body: data,
            credentials: 'include',
            method: 'POST'
        })
        .then(onSuccess, onError);
    }
}