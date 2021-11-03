import axios from '../common/request';
import qs from 'qs';

const serviceName="tenant";
const tenantService={
    getList:(params)    =>axios.get(`/api/${serviceName}/tenant`,params),
    getDetail:(params)  =>axios.get(`/api/${serviceName}/tenant`,params),
    register:(data)        =>axios.post(`/api/${serviceName}/tenant/register`,data),
    put:(data)          =>axios.put(`/api/${serviceName}/tenant`,data),
    delete:(params)  =>axios.delete(`/api/${serviceName}/tenant`,params),
    login:(data)        =>axios.post(oidc_config.authority+`/connect/token`,qs.stringify(data),{headers: {'Content-Type': 'application/x-www-form-urlencoded'}}),
};

export default tenantService;