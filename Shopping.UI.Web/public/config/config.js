var LOCALHOST_IP = "192.168.1.238";
var oidc_config = {
    authority: "http://" + LOCALHOST_IP+":5101",
    client_id: "productjs",
    redirect_uri: "http://" + LOCALHOST_IP +":5201/#/logincallback",
    response_type: "code",
    scope:"openid profile orderapi productapi memberapi",
    post_logout_redirect_uri: "http://" + LOCALHOST_IP +":5201/#/login",
};

window.oidc_config=oidc_config;
window.apiurl="http://"+LOCALHOST_IP+":5200";
window.TenantId="3a00a01f-8a3b-9d59-a59c-281e8bb589bf";