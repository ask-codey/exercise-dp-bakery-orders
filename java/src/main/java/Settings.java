// The shop's settings. The whole app reaches them through Settings.instance().
class Settings {
    private static Settings instance;
    int taxPercent;

    Settings(int taxPercent) {
        this.taxPercent = taxPercent;
    }

    static Settings instance() {
        if (instance == null) {
            instance = new Settings(8);
        }
        return instance;
    }
}
