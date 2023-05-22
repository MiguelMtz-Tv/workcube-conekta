export class Server {

    // OMITIR VERSION ANGULAR : ng config -g cli.warnings.versionMismatch false
    public static base(): string {
        return "https://localhost:44484/"
    }
    
    public static ip() : string {
        let site = location.origin;
        switch (site) {
            case 'https://eos.workcubeapp.com':
                return "https://gavsa.workcubeapp.com/" // GAVSA SSL;

            case 'http://eos.workcubeapp.com':
                return "https://gavsa.workcubeapp.com/" // GAVSA;

            case 'http://eos.hl.workcubeapp.com':
                return "http://3.134.210.42:2024/" // HEAVY;

            case 'http://eos.zaysa.workcubeapp.com':
                return "http://3.134.210.42:2029/" // ZAYSA;

            case 'http://eos.cdg.workcubeapp.com':
                return "http://3.134.210.42:2026/" // CDG;

            case 'http://eos.demo.workcubeapp.com':
                return "http://demo.workcubeapp.com/" // DEMO;
                
            default:
                return "https://localhost:44484/"; // LOCAL;
        }
    }
}