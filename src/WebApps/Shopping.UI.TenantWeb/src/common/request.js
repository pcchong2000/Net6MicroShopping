import axios from 'axios';

axios.defaults.baseURL = window.apiurl;


axios.interceptors.request.use(function (config) {
// Do something before request is sent
    let token=localStorage.getItem("access_token");
    if(token){
        config.headers['Authorization'] = "Bearer "+token;
    }
    
    return config;
}, function (error) {
// Do something with request error
    return Promise.reject(error);
});

// Add a response interceptor
axios.interceptors.response.use(function (response) {

    return response;
}, function (error) {
    //console.log(JSON.stringify(error));
    return Promise.reject(error);
});

const proxyAxios={
    async get(url,params){
       let resp = await axios.get(this.paramsToUrl(url,params));
       return resp.data;
    },
    async delete(url,params){
        let resp =  await axios.delete(this.paramsToUrl(url,params));
        return resp.data;
    },
    async post(url,data,headers){
        let resp =  await axios.post(url,data,headers)
        return resp.data;
    },
    async put(url,data){
        let resp =  await axios.put(url,data)
        return resp.data;
    },
    
    paramsToUrl(url,params){
        if(params){
            for(let key in params){
                if(url.indexOf("?")==-1){
                    url=url+"?";
                }else{
                    url=url+"&";
                }
                url=url+key+"="+params[key];
            }
        }
        return url;
    }
};

export default proxyAxios;