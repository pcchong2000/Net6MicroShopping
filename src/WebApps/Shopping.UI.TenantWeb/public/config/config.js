var LOCALHOST_IP = "localhost";
var oidc_config = {
    authority: "https://" + LOCALHOST_IP+":5102",
    client_id: "tenantjs",
    redirect_uri: "http://192.168.1.100:5202/#/logincallback",
    response_type: "code",
    scope:"openid profile orderapi productapi memberapi",
    post_logout_redirect_uri: "http://" + LOCALHOST_IP +":5202/#/login",
};

window.oidc_config=oidc_config;
window.apiurl="http://"+LOCALHOST_IP+":5200";
window.fileUploadApi=window.apiurl+"/api/oss/file";