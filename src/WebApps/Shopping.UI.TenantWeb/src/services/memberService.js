import axios from '../common/request';
const serviceName="/api/member/tenant";
const memberService={
    getList:(params)    =>axios.get(`${serviceName}/member`,params),
    getDetail:(params)  =>axios.get(`${serviceName}/member`,params),
    post:(data)        =>axios.post(`${serviceName}/member`,data),
    put:(data)          =>axios.put(`${serviceName}/member`,data),
    delete:(params)  =>axios.delete(`${serviceName}/member`,params),
};

export default memberService;