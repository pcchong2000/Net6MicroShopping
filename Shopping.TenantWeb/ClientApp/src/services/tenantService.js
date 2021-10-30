import axios from '../common/request';

const serviceName="tenant";
const tenantService={
    getList:(params)    =>axios.get(`/api/${serviceName}/tenant`,params),
    getDetail:(params)  =>axios.get(`/api/${serviceName}/tenant`,params),
    post:(data)        =>axios.post(`/api/${serviceName}/tenant`,data),
    put:(data)          =>axios.put(`/api/${serviceName}/tenant`,data),
    delete:(params)  =>axios.delete(`/api/${serviceName}/tenant`,params),
};

export default tenantService;