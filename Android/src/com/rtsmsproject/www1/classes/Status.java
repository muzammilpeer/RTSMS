package com.rtsmsproject.www1.classes;

public class Status {
    // 1 == Safe Zone, 0 == Danger Zone
    private Boolean AlertType;
    private Double Distance;
    // 1 == Miles, 0 == KM
    private Boolean UnitTypeDistance;

    public Boolean getAlertType() {
        return AlertType;
    }
    public void setAlertType(Boolean asOf) {
    	AlertType = asOf;
    }
    public Double getDistance() {
        return Distance;
    }
    public void setDistance(Double asOf) {
    	Distance = asOf;
    }
    public Boolean getUnitTypeDistance() {
        return UnitTypeDistance;
    }
    public void setUnitTypeDistance(Boolean asOf) {
    	UnitTypeDistance = asOf;
    }

}
