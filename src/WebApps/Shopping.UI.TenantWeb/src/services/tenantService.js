import axios from '../common/request';
import qs from 'qs';

const serviceName="/api/tenant";
const tenantService={
    getList:(params)    =>axios.get(`${serviceName}/tenant`,params),
    getDetail:(params)  =>axios.get(`${serviceName}/tenant`,params),
    register:(data)    =>axios.post(`${serviceName}/tenant/register`,data),
    put:(data)          =>axios.put(`${serviceName}/tenant`,data),
    delete:(params)  =>axios.delete(`${serviceName}/tenant`,params),
    login:(data)        =>axios.post(oidc_config.authority+`/connect/token`,qs.stringify(data),{headers: {'Content-Type': 'application/x-www-form-urlencoded'}}),
};

export default tenantService;